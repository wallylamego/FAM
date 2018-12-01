using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Trailers
{
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<Trailer> Trailer { get;set; }

        public async Task OnGetAsync()
        {
            Trailer = await _context.Trailers
               .Include(tt => tt.TrailerType)
                .AsNoTracking()
                .ToListAsync();
        }
        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "VehicleID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var TrailerQuery = _context.Trailers
               .Select(tral => new
               {
                   tral.VehicleID,
                   tral.FleetNo,
                   tral.InsuranceExpiry,
                   tral.LicenseExpiry,
                   tral.LinkRegistrationNumber,
                   tral.LinkVinNo,
                   tral.RegistrationNumber,
                   tral.VinNo
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
            var Result = TrailerQuery
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
    
}
}
