using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Fuel
    {
        public int FuelID { get; set; }
        public int TripID { get; set; }
        public string PurchaseOrderID { get; set; }
        public double FuelRate { get; set; }
        public double Litres { get; set; }
        public double Odometre { get; set; }

        public Trip Trip { get; set; }

    }
}
