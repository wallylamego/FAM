using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Province
    {
        public int ProvinceID { get; set; }
        [Required]
        public int CountryID { get; set; }
        [Required]
        [Display(Name ="Province")]
        [MaxLength(40)]
        public string ProvinceName { get; set; }

        public Country Country { get; set; }
    }
}
