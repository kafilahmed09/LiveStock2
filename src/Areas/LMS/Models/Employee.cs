using BES.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Areas.LMS.Models
{
    [Table("Employees", Schema = "HR")]
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public short Gender { get; set; }
        public string ContactNo { get; set; }
        [DisplayName("Supervisor/Section Head")]
        public int SupervisorID { get; set; }
        [ForeignKey("SectionID")]
        public short SectionID { get; set; }
        public virtual Section Section { get; set; }
    }
}
