using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("AddendumType", Schema = "Proc")]
    public class AddendumType
    {
        [Key]
        [DisplayName("Addendum Type")]
        public short AddendumTypeId { get; set; }

        [DisplayName("Addendum Type")]
        public string Name { get; set; }
        public short Code { get; set; }
    }
}