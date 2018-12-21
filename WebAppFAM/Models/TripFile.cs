using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class TripFile
    {
        public int TripFileID { get; set; }
        public int TripID { get; set; }
        public string TripFileName { get; set; }
        public string FilePath { get; set; }
        public string FileDateTime { get; set; }
        public Trip Trip { get; set; }
    }
}
