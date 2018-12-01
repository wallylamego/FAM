using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        [Required]
        [MaxLength(40)]
        [Display(Name ="Country")]
        public string CountryName{get;set;}
    }
}
