using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<Driver> Driver { get;set; }

        public async Task OnGetAsync()
        {
            Driver = await _context.Drivers.ToListAsync();
        }
        //This get provides a list of Paged Drivers
        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
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
            var Result = DriverQuery
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
        public IActionResult OnDeleteDelete([FromBody] Driver obj)
        {
            if (obj != null)
            {
                _context.Drivers.Remove(obj);
                _context.SaveChanges();
                return new JsonResult("Driver removed successfully");
            }
            else
            {
                return new JsonResult("Driver not removed.");
            }
        }
    }
}
