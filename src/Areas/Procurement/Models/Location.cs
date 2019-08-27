using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Location", Schema = "Proc")]
    public class Location
    {
        [Key]
        public short LocationId { get; set; }

        [DisplayName("Location")]
        public string Name { get; set; }
    }
}