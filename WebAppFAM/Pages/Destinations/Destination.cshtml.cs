using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Destinations
{
    [Authorize]
    public class DestinationModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
        public SelectList CustomerNameSL { get; set; }

        public DestinationModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Destination Destination { get; set; }

        public async Task<IActionResult> OnGetAsync(int? destinationId)
        {
            PopulateCustomerDropDownList();

            if (destinationId == null)
             {
                return Page();
             }
            else
            {
                Destination = await _context.Destinations
                .Include(s => s.StartLocation)
                .Include(e => e.EndLocation)
                .Include(c=>c.Customer)
                .SingleOrDefaultAsync(m => m.DestinationID == destinationId);
                return new JsonResult(Destination);
            }
        }

        //This get provides a list of Paged Destinations
        public async Task<JsonResult> OnPostPaging([FromForm] DataTableAjaxPostModel Model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            DataTableAjaxPostModel.GetOrderByParameters(Model.order, Model.columns, "DestinationID",
                out bool SortDir, out string SortBy);


            //First create the View of the new model you wish to display to the user
            var DestinationQuery =  _context.Destinations
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
            var Result = await DestinationQuery
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

        public async Task<IActionResult> OnPostInsert([FromBody] Destination obj)
        {
            if (obj != null)
            {
                try
                {
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Destination has been created successfully.");
                }
                catch (DbUpdateException d)
                {
                    return new JsonResult("New Destination not created." + d.InnerException.Message);
                }
            }
            
            else
            {
                return new JsonResult("Insert Destination was null");
            }

        }
        public async Task<IActionResult> OnPutUpdate([FromBody] Destination obj)
        {
            try
            {
                _context.Attach(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new JsonResult("Destination updated.");
            }
            catch(DbUpdateException d)
            {
                return new JsonResult("Destination not update." + d.InnerException.Message);
            }
        }

        public async Task<IActionResult> OnDeleteDelete([FromBody] Destination obj)
        {
            if (obj != null && HttpContext.User.IsInRole("Admin"))
            {
                try
                {
                    _context.Destinations.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Destination removed successfully");
                }
                catch(Exception e)
                {
                    return new JsonResult("Destination not removed. There was a database error :" + e.Message);
                }
            }
            else
            {
                return new JsonResult("Destination not removed.");
            }
    
        }

        public void PopulateCustomerDropDownList(object selectedCustomer = null)
        {
            var CustomerQuery = from c in _context.Customers
                                orderby c.Name
                                select c;
            CustomerNameSL = new SelectList(CustomerQuery.AsNoTracking(),
                        "CustomerID", "Name", selectedCustomer);

        }


        public JsonResult OnGetCustomerList()
        {
            List<Customer> CustomerList = _context.Customers.ToList();
            return new JsonResult(CustomerList);
        }


    }
}