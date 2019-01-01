using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.SubContractors
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DeleteModel(WebAppFAM.Models.WebAppFAMContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubContractor = await _context.SubContractor.FindAsync(id);

            if (SubContractor != null)
            {
                _context.SubContractor.Remove(SubContractor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
