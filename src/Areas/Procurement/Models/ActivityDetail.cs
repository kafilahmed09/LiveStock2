using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("ActivityDetail", Schema = "Proc")]
    public class ActivityDetail
    {
        [Key]
        [DisplayName("Step")]
        public short StepID { get; set; }
        [Key]
        [DisplayName("Activity")]
        public short ActivityID { get; set; }

        [DisplayName("Not Applicable")]
        public bool NotApplicable { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Planned")]
        public DateTime? PlannedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Actual")]
        public DateTime? ActualDate { get; set; }

        [DisplayName("Attachment")]
        public string Attachment { get; set; }
        //[ScaffoldColumn(false)]
        //public short Crea { get; set; }
        [ScaffoldColumn(false)]

        [DisplayName("Creadted By")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? UpdatedDate { get; set; }

        [DisplayName("Updated By")]
        public string UpdatedBy { get; set; }
        public virtual Step Step {get;set;}
        public virtual Activity Activity { get; set; }

    }
}