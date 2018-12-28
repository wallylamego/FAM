using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Horses
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public CreateModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Horse Horse { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Horses.Add(Horse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}