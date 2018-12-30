using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Fuel
    {
        public int FuelID { get; set; }
        public int TripID { get; set; }

        [Required(ErrorMessage = "Purchase Order Number is required")]
        public string PurchaseOrderID { get; set; }

        [Required(ErrorMessage = "Fuel Rate is required")]
        [Range(0.01, 500.00, ErrorMessage = "Rate must be less than 500")]
        public double FuelRate { get; set; }

        [Required(ErrorMessage = "Litres are required")]
        [Range(0.01, 1000.00, ErrorMessage = "Litres must be less than 1000")]
        public double Litres { get; set; }

        [Required(ErrorMessage = "The odometer is a required field")]
        [Range(0.01, 99999999999.99, ErrorMessage = "Please enter a number")]
        public double Odometre { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedUtc { get; set; }

        public Trip Trip { get; set; }

    }
}
