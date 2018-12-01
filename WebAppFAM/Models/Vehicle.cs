using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public abstract class Vehicle

    {
        public int VehicleID { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Vin Number")]
        public string VinNo { get; set; }

        [Display(Name ="License Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LicenseExpiry { get; set; }

        [Display(Name = "Insurance Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InsuranceExpiry { get; set; }

        [Required]
        [MaxLength(6)]
        [Display(Name = "Fleet Number")]
        public string FleetNo { get; set; }

    }
}
