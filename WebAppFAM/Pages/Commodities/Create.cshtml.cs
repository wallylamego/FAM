using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Commodities
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly WebAppFAM.Data.ApplicationDbContext _context;

        public CreateModel(WebAppFAM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Commodity Commodity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Commodity.Add(Commodity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}