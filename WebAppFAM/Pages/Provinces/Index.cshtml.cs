using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Provinces
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<Models.Province> Province { get;set; }

        public async Task OnGetAsync()
        {
            Province = await _context.Provinces
                .Include(p => p.Country).OrderBy(p=>p.CountryID).ToListAsync();
        }
        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "ProvinceID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var ProvinceQuery = _context.Provinces.Include(c=>c.Country)
               .Select(prv => new
               {
                   prv.ProvinceID,
                   prv.Country.CountryName,
                   prv.ProvinceName
               }
               );

            totalResultsCount = ProvinceQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                ProvinceQuery = ProvinceQuery
                        .Where(
                p => p.ProvinceName.ToLower().Contains(Model.search.value.ToLower()) ||
                        p.CountryName.ToString().ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = ProvinceQuery.Count();
            }
            var Result = ProvinceQuery
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
        public IActionResult OnDeleteDelete([FromBody] Province obj)
        {
            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                _context.Provinces.Remove(obj);
                _context.SaveChanges();
                return new JsonResult("Province removed successfully");
            }
            else
            {
                return new JsonResult("Province not removed.");
            }
        }
    }
}
