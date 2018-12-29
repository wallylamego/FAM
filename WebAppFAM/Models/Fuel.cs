using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Fuel
    {
        public int FuelID { get; set; }
        public int TripID { get; set; }
        public string PurchaseOrderID { get; set; }

        [Required(ErrorMessage = "Fuel Rate is required")]
        [Range(0.01, 1000.00, ErrorMessage = "Rate must be less than 500")]
        public double FuelRate { get; set; }

        [Required(ErrorMessage = "Litres are required")]
        [Range(0.01, 1000.00, ErrorMessage = "Litres must be less than 1000")]
        public double Litres { get; set; }

       // [Range(0.01, 99999999999.99, ErrorMessage = "Please enter a number")]
        public double Odometre { get; set; }

        public Trip Trip { get; set; }

    }
}
