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
using System.Diagnostics;

namespace WebAppFAM.Pages.Trips
{
    public class TripModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        private IHostingEnvironment _hostingEnvironment;
        private readonly string _newPath;
        // private string _newPath;


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
            _newPath = Path.Combine(webRootPath, folderName);

        }

        [BindProperty]
        public Trip Trip { get; set; }
        public DateTimeUtilities DTU;
       
        public async Task<IActionResult> OnGetAsync(int tripID)
        {

            Trip = await _context.Trips
                .Include(l => l.Destination.StartLocation)
                .Include(c => c.Destination.EndLocation)
                .Include(d=> d.Driver)
                .Include(h=>h.Horse)
                .Include(t=>t.Trailer)
                .SingleOrDefaultAsync(m => m.TripID == tripID);
           
            if (Trip == null)
            {
                Trip = new Trip();
            }
            return Page();
        }


        #region FuelUpdates
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
               ).Where(FuelItem => FuelItem.TripID == Convert.ToInt32(Model.search.value));

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
        #endregion

        #region UpdateTrip
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
        #endregion

        #region FilesUpdates
        //Add a new File Item for this Trip
        public IActionResult OnPostInsertFileItem([FromBody] TripFile obj)
        {
            if (obj != null)
            {
                _context.Add(obj);
                _context.SaveChanges();
                return new JsonResult(obj);
            }
            else
            {
                return new JsonResult("TripFile added not added");
            }
        }
        public IActionResult OnDeleteDeleteTripFileItem([FromBody] TripFile obj)
        {
            if (obj != null)
            {
                _context.TripFiles.Remove(obj);
                _context.SaveChanges();
                string targetFilePath = Path.Combine(_newPath, obj.TripFileName);
                if (Directory.Exists(Path.GetDirectoryName(targetFilePath)))
                {
                    try
                    {
                        System.IO.File.Delete(targetFilePath);
                    }
                    catch (System.IO.IOException e)
                    {
                        return new JsonResult("Unable to Delete Trip File: " + e.Message);
                    }
                }
                return new JsonResult("Trip File Item removed successfully");
            }
            else
            {
                return new JsonResult("Trip File Item not removed.");
            }

        }
        public JsonResult OnPostTripFilePaging([FromForm] DataTableAjaxPostModel Model,
            [FromForm] Trip TripToSave)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "TripFileID",
                out bool SortDir, out string SortBy);

            //First create the View of the new model you wish to display to the user
            var TripFileQuery = _context.TripFiles
               .Select(TripFileItem => new
               {
                   TripFileItem.TripFileID,
                   TripFileItem.TripID,
                   TripFileItem.TripFileName,
                   TripFileItem.FileDateTime,
                   TripFileItem.FilePath
               }
               ).Where(TripFileItem => TripFileItem.TripID == Convert.ToInt32(Model.search.value));

            totalResultsCount = TripFileQuery.Count();
            filteredResultsCount = totalResultsCount;

            var Result = TripFileQuery.ToList();
                       
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
        #endregion


    }
}
