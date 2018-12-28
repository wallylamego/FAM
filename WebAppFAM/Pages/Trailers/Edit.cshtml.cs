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


namespace WebAppFAM.Pages.Trailers
{
    [Authorize]
    public class EditModel : TrailerTypeNamePageModel
    {
        private readonly WebAppFAMContext _context;
        
        public EditModel(WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trailer Trailer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trailer = await _context.Trailers
                .Include(tt => tt.TrailerType).FirstOrDefaultAsync(m => m.VehicleID == id);
               
            if (Trailer == null)
            {
                return NotFound();
            }
            PopulateTrailerTypeDropDownList(_context, Trailer.TrailerTypeID);
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            var TrailerToUpdate = await _context.Trailers.FindAsync(id);

            if (await TryUpdateModelAsync<Trailer>(
                 TrailerToUpdate,
                    "trailer",   // Prefix for form value.
                    t => t.FleetNo,
                    t => t.InsuranceExpiry, t => t.LicenseExpiry,
                    t => t.LinkRegistrationNumber, t => t.LinkVinNo,
                 t => t.RegistrationNumber, t => t.TrailerTypeID,
                 t => t.VinNo))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select TrailerID if TryUpdateModelAsync fails.
            PopulateTrailerTypeDropDownList(_context, TrailerToUpdate.TrailerTypeID);
            return Page();

        }

        private bool TrailerExists(int id)
        {
            return _context.Trailers.Any(e => e.VehicleID == id);
        }
    }
}
