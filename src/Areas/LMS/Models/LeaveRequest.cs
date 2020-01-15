using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Areas.LMS.Models
{
    [Table("LeaveRequests",Schema ="HR")]
    public class LeaveRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveRequestID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public short TotalDays { get; set; }
        [ForeignKey("EmpLeaveSummaryID")]
        public int EmpLeaveSummaryID { get; set; }
        public virtual EmpLeaveSummary EmpLeaveSummary { get; set; }
        [ForeignKey("LeaveTypeID")]
        public short LeaveTypeID { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
