using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;
using WebAppFAM.Filters;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Microsoft.Net.Http.Headers;


namespace WebAppFAM.Pages.Trips
{
    public class TripModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        private IHostingEnvironment _hostingEnvironment;
        private readonly string _newPath;
        private readonly ILogger<TripModel> _logger;

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public TripModel(WebAppFAM.Models.WebAppFAMContext context, 
                IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string _newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(_newPath))
            {
                Directory.CreateDirectory(_newPath);
            }
        }

        [BindProperty]
        public Trip Trip { get; set; }
        public DateTimeUtilities DTU;

        //Add a new Fuel Item for this Trip
        public IActionResult OnPostInsertFuelItem([FromBody] Fuel obj)
        {
            if (obj != null)
            {
                _context.Add(obj);
                _context.SaveChanges();
                return new JsonResult(obj);
            }
            else
            {
                return new JsonResult("Fuel Item not added");
            }
        }
        public IActionResult OnDeleteDeleteFuelItem([FromBody] Fuel obj)
        {
            if (obj != null)
            {
                _context.FuelItems.Remove(obj);
                _context.SaveChanges();
                return new JsonResult("Fuel Item removed successfully");
            }
            else
            {
                return new JsonResult("Fuel Item not removed.");
            }

        }
        public JsonResult OnPostFuelPaging([FromForm] DataTableAjaxPostModel Model, 
            [FromForm] Trip TripToSave)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "FuelID",
                out bool SortDir, out string SortBy);

            //First create the View of the new model you wish to display to the user
            var FuelQuery = _context.FuelItems
               .Select(FuelItem => new
               {
                   FuelItem.FuelID,
                   FuelItem.TripID,
                   FuelItem.FuelRate,
                   FuelItem.Litres,
                   FuelItem.Odometre,
                   FuelItem.PurchaseOrderID,
               }
               ).Where(FuelItem=> FuelItem.TripID == Convert.ToInt32(Model.search.value));

            totalResultsCount = FuelQuery.Count();
            filteredResultsCount = totalResultsCount;

           // if (!string.IsNullOrEmpty(Model.search.value))
            //{
               

             //   filteredResultsCount = FuelQuery.Count();
            //}
            var Result = FuelQuery
              //          .Skip(Model.start)
                //        .Take(Model.length)
                  //      .OrderBy(SortBy, SortDir)
                        .ToList();

            var value = new
            {
                // this is what datatables wants sending back
               // draw = Model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = Result
            };
            return new JsonResult(value);
        }


        public IActionResult OnPutUpdateTrip([FromBody] Trip obj)
        {
           var Destination = _context.Destinations.FromSql("SELECT * FROM [dbo].[Destinations] WHERE " +
                 " [DestinationID] = {0} ", obj.DestinationID).FirstOrDefault();


            DTU = new DateTimeUtilities(Destination.Distance);

            DTU.SetTripDateTimeStatistics(obj);
            _context.Attach(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return new JsonResult(obj);
        }

        public ActionResult OnPostUpload(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                //string folderName = "Upload";
                //string webRootPath = _hostingEnvironment.WebRootPath;
                //string newPath = Path.Combine(webRootPath, folderName);
                //if (!Directory.Exists(newPath))
                //{
                //    Directory.CreateDirectory(_newPath);
                //}
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(_newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                    }
                }
                return this.Content("Success");
            }
            return this.Content("Fail");
        }

        public IActionResult OnPostInsertTrip([FromBody] Trip obj)
        {

            if (obj != null)
            {
                _context.Add(obj);
                _context.SaveChanges();
                int id = obj.TripID; // Yes it's here
                return new JsonResult(obj);
            }

            else
            {
                return new JsonResult("Insert Destination was null");
            }

        }


        #region StreamingUpload
        // 1. Disable the form value model binding here to take control of handling 
        //    potentially large files.
        // 2. Typically antiforgery tokens are sent in request body, but since we 
        //    do not want to read the request body early, the tokens are made to be 
        //    sent via headers. The antiforgery token filter first looks for tokens
        //    in the request header and then falls back to reading the body.
        [HttpPost]
        [DisableFormValueModelBinding]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StreamingUpload()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the 
            // request.
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;
            

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        //targetFilePath = Path.GetTempFileName();
       
                        string fileName = contentDisposition.FileName.ToString().Trim('"');
                        targetFilePath = Path.Combine(_newPath, fileName);
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);

                            _logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        // Content-Disposition: form-data; name="key"
                        //
                        // value

                        // Do not limit the key name length here because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.ToString(), value);

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            //// Bind form data to a model
            //var user = new User();
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);

            //var bindingSuccessful = await TryUpdateModelAsync(user, prefix: "",
            //    valueProvider: formValueProvider);
            //if (!bindingSuccessful)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //}

            //var uploadedData = new UploadedData()
            //{
            //    Name = user.Name,
            //    Age = user.Age,
            //    Zipcode = user.Zipcode,
            //    FilePath = targetFilePath
            //};
            return new JsonResult(targetFilePath);
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
    #endregion



}
