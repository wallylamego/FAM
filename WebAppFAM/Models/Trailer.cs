using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Trailer: Vehicle
    {
        
        public TrailerType TrailerType { get; set; }
        
        public int TrailerTypeID { get; set; }
       
        // [Required]
        [MaxLength(10)]
        [Display(Name = "Link Registration Number")]
        public string LinkRegistrationNumber { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Link Vin Number")]
        public string LinkVinNo { get; set; }

    }
}
