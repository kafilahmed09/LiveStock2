using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Areas.LMS.Models
{
    [Table("EmpLeaveSummaries", Schema ="HR")]
    public class EmpLeaveSummary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpLeaveSummaryID { get; set; }
        public short Total { get; set; }
        public short Availed { get; set; }
        
        [ForeignKey("EmployeeID")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        [ForeignKey("LeaveTypeID")]
        public short LeaveTypeID { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
