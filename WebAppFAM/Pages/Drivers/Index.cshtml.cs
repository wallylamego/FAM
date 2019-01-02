using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Drivers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public IndexModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Driver> Driver { get;set; }

        
        //This get provides a list of Paged Drivers
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "DriverID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var DriverQuery = _context.Drivers
               .Select(driv => new
               {
                   driv.DriverID,
                   driv.FirstName,
                   driv.Surname,
                   driv.CellNumber,
                   driv.IDNumber,
                   driv.MedicalExpiryDate,
                   driv.NextofKin,
                   driv.NextofKinDate,
                   driv.PassportNo,
                   driv.PDPExpiryDate,
                   driv.SecondName
               }
               );

            totalResultsCount = DriverQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                DriverQuery = DriverQuery
                        .Where(
                d => d.FirstName.ToLower().Contains(Model.search.value.ToLower()) ||
                        d.Surname.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        d.SecondName.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        d.CellNumber.ToString().ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = DriverQuery.Count();
            }
            var Result = await DriverQuery
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

        public async Task<IActionResult> OnDeleteDelete([FromBody] Driver obj)
        {
            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Drivers.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Driver removed successfully");
                }
                catch(DbUpdateException d)
                {
                    return new JsonResult("Driver not removed." + d.InnerException.Message);
                }
            }
            else
            {
                return new JsonResult("Driver not removed.");
            }
        }
    }
}
