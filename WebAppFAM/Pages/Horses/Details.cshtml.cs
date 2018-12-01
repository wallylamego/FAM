using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Horses
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DetailsModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

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
    }
}
