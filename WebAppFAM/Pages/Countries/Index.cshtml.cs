using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Countries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public IndexModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Country> Country { get;set; }
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "CountryID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var CountryQuery = _context.Countries
               .Select(cty => new
               {
                   cty.CountryID,
                   cty.CountryName
               }
               );

            totalResultsCount = CountryQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                CountryQuery = CountryQuery
                        .Where(
                c => c.CountryName.ToLower().Contains(Model.search.value.ToLower())
                       );

                filteredResultsCount = CountryQuery.Count();
            }
            var Result = await CountryQuery
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

        public async Task<IActionResult> OnDeleteDelete([FromBody] Country obj)
        {

            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Countries.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Country removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Country not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Country not removed.");
            }
        }


        public async Task OnGetAsync()
        {
            Country = await _context.Countries.ToListAsync();
        }
    }
    
    
}
