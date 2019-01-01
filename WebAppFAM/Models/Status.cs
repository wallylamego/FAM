using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Status
    {
        public int StatusID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedUtc { get; set; }
    }
}
