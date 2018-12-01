using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Destinations
{
    public class DestinationPartialModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        public SelectList CustomerNameSL { get; set; }

        public DestinationPartialModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            
            _context = context;
        }

        [BindProperty]
        public Destination Destination { get; set; }

        //This get provides a list of Paged Destinations
        public JsonResult OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {

            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "DestinationID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var DestinationQuery = _context.Destinations
               .Include(s => s.StartLocation)
               .Include(e => e.EndLocation)
               .Include(c => c.Customer)
               .Select(dest => new
               {
                    dest.DestinationID,
                    dest.CustomerID,
                    dest.StartLocationID,
                    dest.EndLocationID,
                    CustomerName = dest.Customer.Name,
                    StartLocationName = dest.StartLocation.Province.Country.CountryName + " : " + dest.StartLocation.Province.ProvinceName + " : " +
                                   dest.StartLocation.LocationName,
                    EndLocationName = dest.EndLocation.Province.Country.CountryName + " : " + dest.EndLocation.Province.ProvinceName + " : " +
                                   dest.EndLocation.LocationName,
                    dest.Distance  
               }
               );
            
            totalResultsCount = DestinationQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                DestinationQuery = DestinationQuery
                        .Where(
                d => d.StartLocationName.ToLower().Contains(Model.search.value.ToLower()) ||
                        d.EndLocationName.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        d.Distance.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        d.CustomerName.ToString().ToLower().Contains(Model.search.value.ToLower()));

                filteredResultsCount = DestinationQuery.Count();
            }
            var Result = DestinationQuery
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