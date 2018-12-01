using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Provinces
{
    public class EditModel : CountryProvinceNamePageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public EditModel(WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Province Province { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Province = await _context.Provinces
                .Include(p => p.Country).SingleOrDefaultAsync(m => m.ProvinceID == id);

            if (Province == null)
            {
                return NotFound();
            }
            PopulateCountryDropDownList(_context, Province.CountryID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            var ProvinceToUpdate = await _context.Provinces.FindAsync(id);

            if (await TryUpdateModelAsync(
                     ProvinceToUpdate,
                     "Province",
                      p => p.ProvinceID, p => p.CountryID,
                      p => p.ProvinceName
                         ))
            {
            
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Select Country ID if TryUpdateSync Fails
            PopulateCountryDropDownList(_context, ProvinceToUpdate.CountryID);
            return Page();
        }

        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.ProvinceID == id);
        }
    }
}
