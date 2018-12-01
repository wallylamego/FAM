using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Commodity
    {
        public int CommodityID { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
