using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;
using WebAppFAM.Data;

namespace WebAppFAM
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public IndexModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "Name",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var CustomerQuery = _context.Customers
               .Select(c => new
               {
                   c.CustomerID,
                   CustomerName = c.Name,
                   c.AccountNo
               }
               );

            totalResultsCount = CustomerQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                CustomerQuery = CustomerQuery
                        .Where(
                c => c.CustomerName.ToLower().Contains(Model.search.value.ToLower())
                       );

                filteredResultsCount = CustomerQuery.Count();
            }
            var Result = await CustomerQuery
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

        public async Task<IActionResult> OnDeleteDelete([FromBody] Customer obj)
        {

            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Customers.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Customer removed successfully");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Customer not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Customer not removed.");
            }
        }
        public async Task OnGetAsync()
        {
            Customer = await _context.Customers.ToListAsync();
        }
    }
}
