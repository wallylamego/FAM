using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Statuses
{
    public class DeleteModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DeleteModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Status Status { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Status = await _context.Status.FirstOrDefaultAsync(m => m.StatusID == id);

            if (Status == null)
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

            Status = await _context.Status.FindAsync(id);

            if (Status != null)
            {
                _context.Status.Remove(Status);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
