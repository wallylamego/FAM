using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Horses
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public IndexModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Horse> Horse { get;set; }

        

        public async Task<IActionResult> OnDeleteDelete([FromBody] Horse obj)
        {
            if (obj != null & HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Horses.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Horse removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Horse not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Horse not removed.");
            }
        }
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;
            
            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "VehicleID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var HorseQuery = _context.Horses
               .Select(hor => new
               {
                   hor.VehicleID,
                   hor.FleetNo,
                   hor.GPSUnitNo,
                   hor.InsuranceExpiry,
                   hor.LicenseExpiry,
                   hor.PhoneNo,
                   hor.RegistrationNumber,
                   hor.VinNo
               }
               );

            totalResultsCount = HorseQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                HorseQuery = HorseQuery
                        .Where(
                h => h.FleetNo.ToLower().Contains(Model.search.value.ToLower()) ||
                        h.RegistrationNumber.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        h.PhoneNo.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        h.GPSUnitNo.ToString().ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = HorseQuery.Count();
            }
            var Result = await HorseQuery
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
    }
}
