using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class Contractor
    {
        [Key]
        public Int16 ContractorID { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Contact")]
        public string Contact { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [Required]
        public string NTNNo { get; set; }
        [Required]
        public string Address { get; set; }
        public Int16 ContractorTypeID { get; set; }

        public virtual ContractorType ContractorType { get; set; }
    }
}