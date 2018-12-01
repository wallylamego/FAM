using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppFAM.Pages.Horses
{
    public class AdminModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Administration Page";
        }
    }
}