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

namespace WebAppFAM.Pages.Locations
{
    [Authorize]
    public class LocationModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;
        
        public LocationModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IList<Location> Location { get;set; }

        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
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
            var Result = await LocationQuery
                        .Skip(Model.start)
                        .Take(Model.length)
                        .OrderBy(SortBy, SortDir)
                        .ToListAsync();

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

       

        public async Task<IActionResult> OnDeleteDelete([FromBody] Location obj)
        {
            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                { 
                    _context.Locations.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Location removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Location not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Location not removed.");
            }

        }


    }

}

