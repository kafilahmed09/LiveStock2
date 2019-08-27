using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class SchoolClass
    {
        [Key]
        public Int16 ClassID { get; set; }

        [DisplayName("Class Name")]
        public string ClassName { get; set; }
    }
}