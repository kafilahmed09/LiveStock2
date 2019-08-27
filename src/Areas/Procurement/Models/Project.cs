using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Project", Schema = "Proc")]
    public class Project
    {
        [Key]
        public short ProjectNo { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        public string GrantNo { get; set; }
        public string ProjectID { get; set; }
    }
}