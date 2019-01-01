using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.SubContractors
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
        public SubContractor SubContractor { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SubContractor.Add(SubContractor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}