using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Unit", Schema = "Proc")]
    public class Unit
    {
        [Key]
        public short UnitId { get; set; }
        public string Name { get; set; }
    }
}