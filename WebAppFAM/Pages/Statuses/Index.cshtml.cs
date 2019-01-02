using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Statuses
{
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public IndexModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Status> Status { get;set; }
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "Name",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var StatusQuery = _context.Status
               .Select(s => new
               {
                   s.StatusID,
                   StatusName = s.Name,
               }
               );

            totalResultsCount = StatusQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                StatusQuery = StatusQuery
                        .Where(
                c => c.StatusName.ToLower().Contains(Model.search.value.ToLower())
                       );

                filteredResultsCount = StatusQuery.Count();
            }
            var Result = await StatusQuery
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

        public async Task<IActionResult> OnDeleteDelete([FromBody] Status obj)
        {

            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Status.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Status removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Status not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Status not removed.");
            }
        }
        public async Task OnGetAsync()
        {
            Status = await _context.Status.ToListAsync();
        }
    }
}
