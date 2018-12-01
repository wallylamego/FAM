using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Trips
{
    public class TripModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        

        public TripModel(WebAppFAM.Models.WebAppFAMContext context)
        {

            _context = context;
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


        public void OnGet()
        {

        }
    }
}