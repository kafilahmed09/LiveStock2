using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Method", Schema = "Proc")]
    public class Method
    {
        [Key]
       public short MethodID { get; set; }

        [DisplayName("Method")]
        public string Name { get; set; }

    }
}