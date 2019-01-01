using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.SubContractors
{
    public class IndexModel : PageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;

        public IndexModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IList<SubContractor> SubContractor { get;set; }

        public async Task OnGetAsync()
        {
            SubContractor = await _context.SubContractor.ToListAsync();
        }
    }
}
