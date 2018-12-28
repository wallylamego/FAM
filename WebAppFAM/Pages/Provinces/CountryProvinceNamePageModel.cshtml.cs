using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Pages.Provinces
{
    [Authorize]
    public class CountryProvinceNamePageModel : PageModel
    {
       
        public SelectList CountryNameSL { get; set; }
        public SelectList ProvinceNameSL { get; set; }

        public void PopulateCountryDropDownList(WebAppFAMContext _context,
            object selectedCountry = null)
        {
            var CountryQuery = from c in _context.Countries
                                    orderby c.CountryName
                                    select c;
            CountryNameSL = new SelectList(CountryQuery.AsNoTracking(),
                        "CountryID", "CountryName", selectedCountry);

        }
        public void PopulateProvinceDropDownList(WebAppFAMContext _context,int id,
           object selectedProvince = null)
        {
            var ProvinceQuery = from c in _context.Provinces
                               where c.CountryID ==id
                               orderby c.ProvinceName
                               select c;
            ProvinceNameSL = new SelectList(ProvinceQuery.AsNoTracking(),
                        "ProvinceID", "ProvinceName", selectedProvince);

        }

        public JsonResult GetProvinceList(WebAppFAMContext _context,int CountryId)
        {
            List<Province> ProvinceList = _context.Provinces.Where(p => p.CountryID == CountryId).ToList();
            JsonResult jsonProvinceList = new JsonResult(ProvinceList);
           
            return jsonProvinceList;
        }

    }
}