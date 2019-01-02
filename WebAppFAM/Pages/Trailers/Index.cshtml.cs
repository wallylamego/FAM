using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Trailers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public IndexModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Trailer> Trailer { get;set; }

        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "VehicleID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var TrailerQuery = _context.Trailers.Include(tt => tt.TrailerType)
               .Select(tral => new
               {
                   tral.VehicleID,
                   tral.FleetNo,
                   tral.InsuranceExpiry,
                   tral.LicenseExpiry,
                   tral.LinkRegistrationNumber,
                   tral.LinkVinNo,
                   tral.RegistrationNumber,
                   tral.VinNo,
                   trailerType = tral.TrailerType.Name
               }
               );

            totalResultsCount = TrailerQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                TrailerQuery = TrailerQuery
                        .Where(
                h => h.FleetNo.ToLower().Contains(Model.search.value.ToLower()) ||
                        h.RegistrationNumber.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        h.VinNo.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        h.LinkRegistrationNumber.ToString().ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = TrailerQuery.Count();
            }
            var Result = await TrailerQuery
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
        public async Task<IActionResult> OnDeleteDelete([FromBody] Trailer obj)
        {
            if (obj != null & HttpContext.User.IsInRole("Admin"))
            {
                try
                { 
                _context.Trailers.Remove(obj);
                await _context.SaveChangesAsync();
                return new JsonResult("Trailer removed successfully");
                }
                catch(DbUpdateException d)
                {
                    return new JsonResult("Trailer not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Trailer not removed because you do not have authorisation to delete trailers.");
            }
        }
    }
}
