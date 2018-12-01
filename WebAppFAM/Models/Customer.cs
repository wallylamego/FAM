using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
