using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Commodities
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<Commodity> Commodity { get;set; }

        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "Name",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var CommodityQuery = _context.Commodity
               .Select(c => new
               {
                   c.CommodityID,
                   CommodityName = c.Name
               }
               );

            totalResultsCount = CommodityQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                CommodityQuery = CommodityQuery
                        .Where(
                c => c.CommodityName.ToLower().Contains(Model.search.value.ToLower())
                       );

                filteredResultsCount = CommodityQuery.Count();
            }
            var Result = await CommodityQuery
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

        public async Task<IActionResult> OnDeleteDelete([FromBody] Commodity obj)
        {

            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Commodity.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Commodity removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Commodity not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Commodity not removed.");
            }
        }


        public async Task OnGetAsync()
        {
            Commodity = await _context.Commodity.ToListAsync();
        }
    }
}
