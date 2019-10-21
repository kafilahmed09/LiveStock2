using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Step", Schema = "Proc")]
    public class Step
    {
        [Key]
        public short StepID { get; set; }
        public short ProcurementPlanID { get; set; }
        [DisplayName("Step Description")]
        public string Name { get; set; }
        [DisplayName("Step#")]
        public short? SerailNo { get; set; }

        public virtual ProcurementPlan ProcurementPlan { get;set;}
    }
}