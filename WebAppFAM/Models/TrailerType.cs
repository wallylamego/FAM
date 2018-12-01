using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class TrailerType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TrailerTypeID { get; set; }
        [MaxLength(40)]
        // [Required]
        [Display(Name ="Trailer Type")]
        public string Name { get; set; }

    }
}
