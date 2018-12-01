using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;
using WebAppFAM;

namespace WebAppFAM.Pages.Locations
{
    public class LocationModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        
        public LocationModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }
        

        public IList<Location> Location { get;set; }

        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {
           
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "LocationName", 
                out bool SortDir, out string SortBy);

            if (SortBy.ToLower().Contains("gps"))
            {
                SortBy = ExtensionMethods.UppercaseSpecifiedNumbers(SortBy, 3);
            }

            //First create the View of the new model you wish to display to the user
            var LocationQuery = _context.Locations
               .Include(p => p.Province)
               .Include(c => c.Province.Country)
               .Select(loc => new 
               {
                   loc.LocationID,
                   loc.Province.Country.CountryName,
                   loc.Province.ProvinceName,
                   loc.LocationName,
                   loc.GPSCoordinates
               }
                   );
               
            totalResultsCount = LocationQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                LocationQuery = LocationQuery
                        .Where(
                l => l.LocationName.ToLower().Contains(Model.search.value.ToLower()) ||
                        l.GPSCoordinates.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        l.CountryName.ToLower().Contains(Model.search.value.ToLower()) ||
                        l.ProvinceName.ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = LocationQuery.Count();
            }
            var Result = LocationQuery
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

        public async Task<JsonResult> OnPostDeleteAsync([FromBody] Location LocToDelete)
        {
            if (LocToDelete == null)
            {
                return new JsonResult("Location Not Found or  already Deleted");
            }

            var LocationToDelete = await _context.Locations.FindAsync(LocToDelete.LocationID);

            if (LocationToDelete != null)
            {
                _context.Locations.Remove(LocationToDelete);
                await _context.SaveChangesAsync();
            }
                return new JsonResult("Location: " + LocationToDelete.LocationName + " Deleted.");
        }

        public IActionResult OnDeleteDelete([FromBody] Location obj)
        {
            if (obj != null)
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

