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
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<Country> Country { get;set; }
        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
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
            var Result = CountryQuery
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

        [Authorize(Roles = "Admin")]
        public IActionResult OnDeleteDelete([FromBody] Country obj)
        {
            if (obj != null)
            {
                _context.Countries.Remove(obj);
                _context.SaveChanges();
                return new JsonResult("Country removed successfully");
            }
            else
            {
                return new JsonResult("Province not removed.");
            }
        }


        public async Task OnGetAsync()
        {
            Country = await _context.Countries.ToListAsync();
        }
    }
    
    
}
