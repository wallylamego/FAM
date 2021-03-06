﻿using System;
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
using Microsoft.AspNetCore.Identity;
using WebAppFAM.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppFAM.Pages.Trips
{
    public class TripModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;
        private readonly string _newPath;
        private readonly UserManager<ApplicationUser> _userManager;
        public SelectList StatusSL { get; set; }
        public SelectList SubContractorSL { get; set; }
        public SelectList CommoditySL { get; set; }

        public void PopulateStatusDropDownList(object selectedStatus = null)
        {
            var StatusQuery = from s in _context.Status
                               orderby s.Name
                               select s;
            StatusSL = new SelectList(StatusQuery.AsNoTracking(),
                        "StatusID", "Name", selectedStatus);
        }
        public void PopulateSubContractorDropDownList(object selectedSubContractor = null)
        {
            var SubContractorQuery = from s in _context.SubContractor
                              orderby s.Name
                              select s;
            SubContractorSL = new SelectList(SubContractorQuery.AsNoTracking(),
                        "SubContractorID", "Name", selectedSubContractor);
        }
        public void PopulateCommodityDropDownList(object selectedCommodity = null)
        {
            var CommodityQuery = from c in _context.Commodity
                                     orderby c.Name
                                     select c;
            CommoditySL = new SelectList(CommodityQuery.AsNoTracking(),
                        "CommodityID", "Name", selectedCommodity);
        }

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public TripModel(WebAppFAM.Data.ApplicationDbContext context,
                IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            _newPath = Path.Combine(webRootPath, folderName);

        }

        [BindProperty]
        public Trip Trip { get; set; }
        [BindProperty]
        public Fuel FuelItem { get; set; }
        public DateTimeUtilities DTU;
       
        public async Task<IActionResult> OnGetAsync(int tripID)
        {

            Trip = await _context.Trips
                .Include(l => l.Destination.StartLocation)
                .Include(c => c.Destination.EndLocation)
                .Include(d=> d.Driver)
                .Include(h=>h.Horse)
                .Include(t=>t.Trailer)
                .Include(u=>u.User)
                .SingleOrDefaultAsync(m => m.TripID == tripID);
           
            if (Trip == null)
            {
                Trip = new Trip();
            }
            PopulateStatusDropDownList();
            PopulateSubContractorDropDownList();
            PopulateCommodityDropDownList();
            return Page();
        }

        #region FuelUpdates
        //Add a new Fuel Item for this Trip
        public async Task<IActionResult> OnPostInsertFuelItem([FromBody] Fuel obj)
        {
            if (obj != null)
            {
                try
                { 
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult(obj);
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Fuel Item not Added." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Fuel Item not added");
            }
        }
        public async Task<IActionResult> OnDeleteDeleteFuelItem([FromBody] Fuel obj)
        {
            if (obj != null)
            {
                try
                { 
                    _context.FuelItems.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Fuel Item removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Fuel Item not Removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Fuel Item not removed.");
            }

        }
        public async Task<IActionResult> OnPostFuelPaging([FromForm] DataTableAjaxPostModel Model,
            [FromForm] Trip TripToSave)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            //DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "FuelID",
            //    out bool SortDir, out string SortBy);
            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "CreatedUtc",
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
                   FuelItem.CreatedUtc
               }
               )
               .Where(FuelItem => FuelItem.TripID == Convert.ToInt32(Model.search.value));

            totalResultsCount = FuelQuery.Count();
            filteredResultsCount = totalResultsCount;


            var Result = await FuelQuery.OrderBy(SortBy, SortDir).ToListAsync();

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
        public async Task<IActionResult> OnPutUpdateTrip([FromBody] Trip obj)
        {
            var Destination = await _context.Destinations.FromSql("SELECT * FROM [dbo].[Destinations] WHERE " +
                  " [DestinationID] = {0} ", obj.DestinationID).FirstOrDefaultAsync();


            DTU = new DateTimeUtilities(Destination.Distance);

            DTU.SetTripDateTimeStatistics(obj);
            try
            {
                _context.Attach(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new JsonResult(obj);
            }
            catch (DbUpdateException d)
            {
                return new JsonResult("Trip Changes not saved." + d.InnerException.Message);
            }
        }

        public async Task<IActionResult> OnPostInsertTrip([FromBody] Trip obj)
        {

            if (obj != null)
            {
                try
                {
                    obj.UserID = _userManager.GetUserId(HttpContext.User);
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                    int id = obj.TripID; // Yes it's here
                    obj.User = await _userManager.GetUserAsync(HttpContext.User);
                    return new JsonResult(obj);
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Trip Not Added." + d.InnerException.Message);
                }
            }

            else
            {
                return new JsonResult("Insert Destination was null");
            }

        }
        #endregion

        #region FilesUpdates
        //Add a new File Item for this Trip
        public async Task <IActionResult> OnPostInsertFileItem([FromBody] TripFile obj)
        {
            if (obj != null)
            {
                try 
                {
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult(obj);
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Trip File not added ." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("TripFile added not added");
            }
        }
        public async Task<IActionResult> OnDeleteDeleteTripFileItem([FromBody] TripFile obj)
        {
            if (obj != null)
            {
                try
                { 
                    _context.TripFiles.Remove(obj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Trip File not removed." + d.InnerException.Message);
                }
                string targetFilePath = Path.Combine(_newPath, obj.TripFileName);
                if (Directory.Exists(Path.GetDirectoryName(targetFilePath)))
                {
                    try
                    {
                        System.IO.File.Delete(targetFilePath);
                    }
                    catch (System.IO.IOException e)
                    {
                        return new JsonResult("Unable to Delete Trip File: " + e.InnerException.Message);
                    }
                }
                return new JsonResult("Trip File Item removed successfully");
            }
            else
            {
                return new JsonResult("Trip File Item not removed.");
            }

        }
        public async Task<JsonResult> OnPostTripFilePaging([FromForm] DataTableAjaxPostModel Model,
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

            var Result = await TripFileQuery.ToListAsync();
                       
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
