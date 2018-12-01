using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Trip
    {
        public int TripID { get; set; }

        [Required]
        public string TripCode { get; set; }

        public Boolean ReverseDestinationID { get; set; }

        [Required]
        public int DestinationID { get; set; }

        public Boolean ReturnTrip { get; set; }
        public int ReturnTripID { get; set; }

        
        public int? DriverID { get; set; }

        
        public int? HorseID { get; set; }
        

        public int? TrailerID { get; set; }
        
        public int? CommodityID { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedCollectionDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ActualCollectionDateTime { get; set; }

        public double DiffCollectionTimeHrs { get; set; }
        public double DiffStartTimeHrs { get; set; }
        public double DiffEndTimeHrs { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedStartDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ActualStartDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedCompletionDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ActualCompletionDateTime { get; set; }

        public string InvoiceNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        public string CustomerReferenceNo { get; set; }
        public double InvoicedKms { get; set; }
        public double InvoiceRate { get; set; }
        public double InvoiceAmount { get; set; }
         


        public Destination Destination { get; set; }
        public ICollection<Fuel> FuelItems { get; set; }
        public ICollection<Kilometre> Kilometres { get; set; }

        public Driver Driver { get; set; }
        public Horse Horse { get; set; }
        public Trailer Trailer { get; set; }
        public Commodity Commodity { get; set; }
    }
}
