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

namespace WebAppFAM.Pages.Horses
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public EditModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Horse Horse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Horse = await _context.Horses.SingleOrDefaultAsync(m => m.VehicleID == id);

            if (Horse == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Horse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseExists(Horse.VehicleID))
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

        private bool HorseExists(int id)
        {
            return _context.Horses.Any(e => e.VehicleID == id);
        }
    }
}
