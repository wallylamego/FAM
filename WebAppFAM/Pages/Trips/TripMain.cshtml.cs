using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;
using WebAppFAM;
using Microsoft.AspNetCore.Authorization;

namespace WebAppFAM.Pages.Trips
{
    [Authorize]
    public class TripMain : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;
        
        public TripMain(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IList<Trip> Trip { get;set; }

        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {
           
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "CreateDate", 
                out bool SortDir, out string SortBy);

           
        //First create the View of the new model you wish to display to the user
            var TripQuery = _context.Trips
               .Select(tp => new 
               {
                   tp.TripID,
                   tp.TripCode,
                   Customer = tp.Destination.Customer.Name,
                   CommodityName = tp.Commodity.Name,
                   tp.ReturnTrip,
                   CreatedBy = tp.User.UserName,
                   CreateDate = tp.CreatedUtc,
                   Status = tp.Status.Name,
                   SubContractor = tp.SubContractor.Name,
                   TripStart = tp.ExpectedCollectionDateTime,
                   From = tp.Destination.StartLocation.Province.Country.CountryName + " | " +
                        tp.Destination.StartLocation.Province.ProvinceName + " | " +
                        tp.Destination.StartLocation.LocationName,
                   To = tp.Destination.EndLocation.Province.Country.CountryName + " | " +
                        tp.Destination.EndLocation.Province.ProvinceName + " | " +
                        tp.Destination.EndLocation.LocationName,
                   tp.Destination.Distance,
                   Driver = tp.Driver.FirstName + "|" + tp.Driver.Surname,
                   Horse = tp.Horse.FleetNo,
                   Trailer = tp.Trailer.FleetNo,
                   tp.ExpectedStartDateTime,
                   tp.ExpectedCompletionDateTime,
                   tp.ActualCollectionDateTime,
                   tp.ActualStartDateTime,
                   tp.ActualCompletionDateTime,
                   tp.DiffCollectionTimeHrs,
                   tp.DiffStartTimeHrs,
                   tp.DiffEndTimeHrs,
                   tp.CustomerReferenceNo,
                   tp.InvoiceNo,
                   tp.InvoiceDate,
                   tp.InvoiceRate,
                   tp.Kilometres,
                   tp.InvoiceAmount
               }
                   );
               
            totalResultsCount = TripQuery.Count();
            filteredResultsCount = totalResultsCount;

            if (!string.IsNullOrEmpty(Model.search.value))
            {
                TripQuery = TripQuery
                        .Where(
                t => t.TripCode.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.TripStart.ToString().ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Driver.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.From.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.To.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Customer.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Trailer.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Horse.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.InvoiceNo.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.Status.ToLower().Contains(Model.search.value.ToLower()) ||
                        t.SubContractor.ToLower().Contains(Model.search.value.ToLower())
                        );

                filteredResultsCount = TripQuery.Count();
            }
            var Result = await TripQuery
                        .OrderBy(SortBy, SortDir)
                        .Skip(Model.start)
                        .Take(Model.length)
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
        
        public async Task<IActionResult> OnDeleteDelete([FromBody] Trip obj)
        {
            var TripToDelete = await _context.Trips.FindAsync(obj.TripID);
            var FuelItemsToDelete = _context.FuelItems.Where(fi => fi.TripID == obj.TripID);
            
            if (TripToDelete != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.FuelItems.RemoveRange(FuelItemsToDelete);
                    await _context.SaveChangesAsync();
                    _context.Trips.Remove(TripToDelete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("Trip not removed." + d.InnerException.Message);
                }
            }
                return new JsonResult("Trip: " + TripToDelete.TripCode + " Deleted.");
        }

       


    }

}

