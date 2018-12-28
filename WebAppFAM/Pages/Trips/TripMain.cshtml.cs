using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;
using WebAppFAM;
using Microsoft.AspNetCore.Authorization;

namespace WebAppFAM.Pages.Trips
{
    [Authorize]
    public class TripMain : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        
        public TripMain(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }
        

        public IList<Trip> Trip { get;set; }

        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {
           
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "TripCode", 
                out bool SortDir, out string SortBy);

           
        //First create the View of the new model you wish to display to the user
            var TripQuery = _context.Trips
               .Select(tp => new 
               {
                   tp.TripID,
                   tp.TripCode,
                   TripStart = tp.ExpectedCollectionDateTime,
                   From = tp.Destination.StartLocation.Province.Country.CountryName + " | " +
                        tp.Destination.StartLocation.Province.ProvinceName + " | " +
                        tp.Destination.StartLocation.LocationName,
                   To = tp.Destination.EndLocation.Province.Country.CountryName + " | " +
                        tp.Destination.EndLocation.Province.ProvinceName + " | " +
                        tp.Destination.EndLocation.LocationName,
                   tp.Destination.Distance,
                   Driver = tp.Driver.FirstName + "|" + tp.Driver.Surname,
                   Horse = tp.Horse.FleetNo
               }
                   );
               
            totalResultsCount = TripQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                TripQuery = TripQuery
                        .Where(
                t => t.TripCode.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.TripStart.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Driver.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.From.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.To.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Horse.ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = TripQuery.Count();
            }
            var Result = TripQuery
                        .Skip(Model.start)
                        .Take(Model.length)
                        .OrderBy(SortBy, SortDir)
                        .ToList();

            var value = new
            {
                // this is what datatables wants sending back
                draw = Model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = Result
            };
            return new JsonResult(value);
        }

        public async Task<JsonResult> OnPostDeleteAsync(Trip TripId)
        {
           
            var TripToDelete = await _context.Trips.FindAsync(TripId);

            if (TripToDelete != null)
            {
                _context.Trips.Remove(TripToDelete);
                await _context.SaveChangesAsync();
            }
                return new JsonResult("Trip: " + TripToDelete.TripCode + " Deleted.");
        }

        public IActionResult OnDeleteDelete([FromBody] Location obj)
        {
            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                _context.Locations.Remove(obj);
                _context.SaveChanges();
                return new JsonResult("Location removed successfully");
            }
            else
            {
                return new JsonResult("Location not removed.");
            }

        }


    }

}

