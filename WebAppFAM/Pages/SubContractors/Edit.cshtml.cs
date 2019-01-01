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

namespace WebAppFAM.Pages.SubContractors
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
        public SubContractor SubContractor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubContractor = await _context.SubContractor.FirstOrDefaultAsync(m => m.SubContractorID == id);

            if (SubContractor == null)
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

            _context.Attach(SubContractor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubContractorExists(SubContractor.SubContractorID))
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

        private bool SubContractorExists(int id)
        {
            return _context.SubContractor.Any(e => e.SubContractorID == id);
        }
    }
}
