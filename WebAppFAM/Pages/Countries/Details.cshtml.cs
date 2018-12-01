using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Countries
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DetailsModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public Country Country { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Country = await _context.Countries.SingleOrDefaultAsync(m => m.CountryID == id);

            if (Country == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
