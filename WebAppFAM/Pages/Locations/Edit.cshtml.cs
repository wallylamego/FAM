using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;
using WebAppFAM.Pages.Provinces;

namespace WebAppFAM.Pages.Locations
{
    public class EditModel : CountryProvinceNamePageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public EditModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int locationId)
        {
          /*  if( locationId == null)
            {
                return NotFound();
            }*/

            Location = await _context.Locations
                .Include(l => l.Province)
                .Include(c => c.Province.Country)
                .SingleOrDefaultAsync(m => m.LocationID == locationId);
            PopulateCountryDropDownList(_context, Location.Province.Country.CountryID);
            PopulateProvinceDropDownList(_context, Location.Province.Country.CountryID, Location.Province.ProvinceID);

            if (Location == null)
            {
                return NotFound();
            }
            return Page();
        }

        
        public IActionResult OnPutUpdate([FromBody] Location obj)
        {
            _context.Attach(obj).State = EntityState.Modified;
            _context.SaveChanges();
            return new JsonResult("Location: " + obj.LocationName + " Changes are saved.");
        }

/*
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(Location.LocationID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
        }
  */ 
    }
    
}
