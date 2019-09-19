using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Models.Data
{
    public class Partner
    {
        [Key]
        public Int16 PartnerID { get; set; }

        [Required]
        [DisplayName("Partner Name")]
        public string PartnerName { get; set; }

        [DisplayName("Partner Description")]
        public string Description { get; set; }
    }
}
