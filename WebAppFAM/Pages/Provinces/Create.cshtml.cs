using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Provinces
{
    [Authorize]
    public class CreateModel : CountryProvinceNamePageModel
    {
        private readonly WebAppFAMContext _context;

        public CreateModel(WebAppFAMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateCountryDropDownList(_context);   
            return Page();
        }

        [BindProperty]
        public Models.Province Province { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var EmptyProvince = new Models.Province();
            
            if (await TryUpdateModelAsync(
                     EmptyProvince,
                     "Province",
                      p=> p.ProvinceID,p=>p.CountryID,
                      p=>p.ProvinceName
                         ))
            {
                _context.Provinces.Add(EmptyProvince);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Select Country ID if TryUpdateSync Fails
            PopulateCountryDropDownList(_context, EmptyProvince.CountryID);
            return Page();
        }

        
    }
}