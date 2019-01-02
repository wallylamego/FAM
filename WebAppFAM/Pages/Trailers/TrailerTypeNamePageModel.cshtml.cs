using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Trailers
{
    public class TrailerTypeNamePageModel : PageModel
    {
        public SelectList TrailerNameSL { get; set; }

        public void PopulateTrailerTypeDropDownList(WebAppFAM.Data.ApplicationDbContext _context,
            object selectedTrailerType = null)
        {
            var TrailerTypesQuery = from tt in _context.TrailerTypes
                                    orderby tt.Name
                                    select tt;
            TrailerNameSL = new SelectList(TrailerTypesQuery.AsNoTracking(),
                        "TrailerTypeID", "Name", selectedTrailerType);

        }

        
    }
}