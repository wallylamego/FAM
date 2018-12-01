using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="Location Name")]
        public string LocationName { get; set; }
        [Display(Name = "GPS Coordinates")]
        public double GPSCoordinates { get; set; }
        public int ProvinceID { get; set; }  

        public Province Province { get; set; }
    }
}
