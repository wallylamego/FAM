using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.SubContractors
{
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<SubContractor> SubContractor { get;set; }
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "Name",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var SubContractorQuery = _context.SubContractor
               .Select(s => new
               {
                   s.SubContractorID,
                   SubContractorName = s.Name,
                   s.AccountNo
               }
               );

            totalResultsCount = SubContractorQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                SubContractorQuery = SubContractorQuery
                        .Where(
                c => c.SubContractorName.ToLower().Contains(Model.search.value.ToLower())
                       );

                filteredResultsCount = SubContractorQuery.Count();
            }
            var Result = await SubContractorQuery
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

        public async Task<IActionResult> OnDeleteDelete([FromBody] SubContractor obj)
        {

            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.SubContractor.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("SubContractor removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("SubContractor not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("SubContractor not removed.");
            }
        }
        public async Task OnGetAsync()
        {
            SubContractor = await _context.SubContractor.ToListAsync();
        }
    }
}
