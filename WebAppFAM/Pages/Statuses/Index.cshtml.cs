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
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<Status> Status { get;set; }

        public async Task OnGetAsync()
        {
            Status = await _context.Status.ToListAsync();
        }
    }
}
