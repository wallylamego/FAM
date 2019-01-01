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

namespace WebAppFAM.Pages.Commodities
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public EditModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Commodity Commodity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Commodity = await _context.Commodity.FirstOrDefaultAsync(m => m.CommodityID == id);

            if (Commodity == null)
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

            _context.Attach(Commodity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommodityExists(Commodity.CommodityID))
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

        private bool CommodityExists(int id)
        {
            return _context.Commodity.Any(e => e.CommodityID == id);
        }
    }
}
