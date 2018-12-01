using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Trailers
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DetailsModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Trailer Trailer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
           Trailer = await _context.Trailers
                .Include(tt=> tt.TrailerType)
                .SingleOrDefaultAsync(m => m.VehicleID == id);

            if (Trailer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
