using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("CNIC XXXXX-XXXXXXX-X")]
        [RegularExpression("^[0-9]{5}-[0-9]{7}-[0-9]$", ErrorMessage = "CNIC No must be valid!")]
        public string CNIC { get; set; }
        [Required]
        [DisplayName("Mobile No 03XXXXXXXXX")]
        [RegularExpression("^[0][3][0-9]{9}$", ErrorMessage = "Mobile No must be in valid format!")]
        public string MobileNo { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
