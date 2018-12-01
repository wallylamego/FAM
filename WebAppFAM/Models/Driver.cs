using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFAM.Models
{
    public class Driver
    {
        public int DriverID { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        public string SecondName { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Cell Phone Number")]
        public string CellNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "PDP Expiry Date")]
        public DateTime PDPExpiryDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Medical Expiry Date")]
        public DateTime MedicalExpiryDate { get; set; }

        [Display(Name = "Passport Number")]
        public string PassportNo { get; set; }

        [Display(Name = "Indentity Number")]
        [MaxLength(20)]
        public string IDNumber { get; set; }

        [Display(Name = "Next of Kin")]
        public string NextofKin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Next of Kin Date")]
        public DateTime NextofKinDate { get; set; }

    }
}
