using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppFAM.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using WebAppFAM.Helpers;
using WebAppFAM.Models;

namespace WebAppFAM.Controllers
{
   
    public class UploadFilesController : Controller
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly WebAppFAM.Data.ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;
        private readonly string _newPath;
        private readonly string _virtualPathFolder;
        
        public UploadFilesController(IHostingEnvironment hostingEnvironment,
            WebAppFAM.Data.ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            _newPath = Path.Combine(webRootPath, folderName);
            _virtualPathFolder = "~/Upload/";
            if (!Directory.Exists(_newPath))
            {
                Directory.CreateDirectory(_newPath);
            }
        }


        public IActionResult Index()
        {
            return View();
        }
        #region StreamingUpload
        // 1. Disable the form value model binding here to take control of handling 
        //    potentially large files.
        // 2. Typically antiforgery tokens are sent in request body, but since we 
        //    do not want to read the request body early, the tokens are made to be 
        //    sent via headers. The antiforgery token filter first looks for tokens
        //    in the request header and then falls back to reading the body.


        [Route("")]
        [Route("UploadFiles")]
        [Route("UploadFiles/StreamingUpload/{id}")]
        [HttpPost]
        [DisableFormValueModelBinding]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StreamingUpload(Int32 id)
        {
            Debug.WriteLine("In Streaming Upload for Trip Number: " + id);
        
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
            string newFileName = "";
            DateTime FileDateTime = new DateTime();
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
                        newFileName = FileHelper.newFileName(id, fileName, out FileDateTime);
                        targetFilePath = Path.Combine(_newPath, newFileName);
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);

                            //  _logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
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

                //add the Trip File Object to the database
                TripFile TripFileItem = new TripFile
                {
                    TripID = id,
                    TripFileName = newFileName,
                    FilePath = targetFilePath,
                    FileDateTime = FileDateTime.ToString()
                };
                    _context.Add(TripFileItem);
                    _context.SaveChanges();
                    
               // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            /// Bind form data to a model
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            return new JsonResult(formValueProvider);
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

        [Route("UploadFiles")]
        [Route("UploadFiles/Download")]
        
        public IActionResult Download(string filename)
        {
            string virtualpath = _virtualPathFolder + filename;
            var NewFile = File(virtualpath, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            return NewFile;
        }



    }

    #endregion
    
}
