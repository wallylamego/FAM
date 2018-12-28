using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Trailers
{
    [Authorize]
    public class CreateModel : TrailerTypeNamePageModel
    {
        private readonly WebAppFAM.Models.WebAppFAMContext _context;
       
        [TempData]
        public string message { get; set; }

        public CreateModel(WebAppFAM.Models.WebAppFAMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            PopulateTrailerTypeDropDownList(_context);
            return Page();

            
        }

     

        [BindProperty]
        public Trailer Trailer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyTrailer = new Trailer();
            
            if (await TryUpdateModelAsync(
                emptyTrailer,
                "trailer",
                t => t.FleetNo,
                t =>t.InsuranceExpiry, t => t.LicenseExpiry,
                t => t.LinkRegistrationNumber, t => t.LinkVinNo,
                t => t.RegistrationNumber, t => t.TrailerTypeID,
                t => t.VinNo))
            {
                _context.Trailers.Add(emptyTrailer);
                await _context.SaveChangesAsync();
                message = $"Trailer Number {Trailer.FleetNo} added";
                return RedirectToPage("./Index");
            }

            //Select TrailerTypeID if TryUpdateModelAsyn fails.
            PopulateTrailerTypeDropDownList(_context, emptyTrailer.TrailerTypeID);
            return Page();
        }
    }
}