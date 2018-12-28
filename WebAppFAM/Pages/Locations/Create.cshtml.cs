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
using WebAppFAM.Pages.Provinces;

namespace WebAppFAM.Pages.Locations
{
    [Authorize]
    public class CreateModel :CountryProvinceNamePageModel
    {

        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public CreateModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateCountryDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }

  /*      public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var EmptyLocation = new Location();

            if (await TryUpdateModelAsync(
                     EmptyLocation,
                     "Location",
                      l => l.ProvinceID, l => l.LocationName,
                      l => l.GPSCoordinates, l=> l.Province.CountryID
                         ))
            {
                _context.Locations.Add(EmptyLocation);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Select Province ID if TryUpdateSync Fails
            PopulateCountryDropDownList(_context, EmptyLocation.Province.CountryID);
            PopulateProvinceDropDownList(_context, EmptyLocation.ProvinceID);
           return Page();
  
        }

        public async Task<IActionResult> OnPostAsyncLocation([FromBody] Location obj)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Location Data is incomplete or inaccurate");
            }

            var EmptyLocation = new Location();

            if (await TryUpdateModelAsync(
                     EmptyLocation,
                     "Location",
                      l => l.ProvinceID, l => l.LocationName,
                      l => l.GPSCoordinates
                         ))
            {
                _context.Locations.Add(EmptyLocation);
                await _context.SaveChangesAsync();
                return new JsonResult("New Location Added");
            }
            
            return new JsonResult("Location Not Added");

        }
*/
        public  IActionResult OnPostInsert([FromBody] Location obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
            return new JsonResult("Location: " + obj.LocationName + " added.");
        }

        public JsonResult OnGetProvinceList(int CountryId)
        {
            return GetProvinceList(_context, CountryId);
        }

        
        

    }

}