using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Provinces
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DetailsModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public Models.Province Province { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Province = await _context.Provinces
                .Include(p => p.Country).SingleOrDefaultAsync(m => m.ProvinceID == id);

            if (Province == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
