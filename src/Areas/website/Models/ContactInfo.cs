using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class ContactInfo
    {
        [Key]
        public int ContactInfoID { get; set; }
        [Required]
        [DisplayName("Phone No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string PhoneNo1 { get; set; }
        [DisplayName("Phone No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string PhoneNo2 { get; set; }
        [DisplayName("Phone No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string PhoneNo3 { get; set; }
        [DisplayName("Phone No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string PhoneNo4 { get; set; }
        [DisplayName("Fax No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string FaxNo1 { get; set; }        
        [DisplayName("Fax No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string FaxNo2 { get; set; }
        [DisplayName("Fax No 081-XXXXXXX")]
        [RegularExpression("^[0][8][1][-][0-9]{7}$", ErrorMessage = "Phone No must be in valid format!")]
        public string FaxNo3 { get; set; }
        [Required]
        [DisplayName("WhatsApp 92XXXXXXX")]
        [RegularExpression("^[0][3][0-9]{9}$", ErrorMessage = "WhatsApp No must be in valid format!")]
        public string WhatsAppNo { get; set; }
        [Required]
        public string Address { get; set; }
        public string Email { get; set; }        
    }
}
