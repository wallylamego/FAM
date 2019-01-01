using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Commodities
{
    public class DetailsModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public DetailsModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public Commodity Commodity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Commodity = await _context.Commodity.FirstOrDefaultAsync(m => m.CommodityID == id);

            if (Commodity == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
