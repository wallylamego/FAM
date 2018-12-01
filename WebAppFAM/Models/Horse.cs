using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Horse:Vehicle
    {
        
        [MaxLength(10)]
        [Display(Name ="Truck Phone Number")]
        public string PhoneNo { get; set; }
        [MaxLength(20)]
        [Display(Name ="GPS Unit Number")]
        public string GPSUnitNo { get; set; }

    }
}
