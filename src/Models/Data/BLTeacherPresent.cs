using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLTeacherPresent
    {
        [Key]
        public int BLTeacherPresentID { get; set; }
        
        [DisplayName("Post")]
        public Int16 BLGeneralID { get; set; }

        [DisplayName("Total Present")]
        public Int16 TeachersPresent { get; set; }

        [DisplayName("Total Absent")]
        public Int16 TeachersAbsent { get; set; }

        [DisplayName("Absent 1-7")]
        public Int16 TeachersAbsent1_7 { get; set; }

        [DisplayName("Absent 7-30")]
        public Int16 TeachersAbsent7_30 { get; set; }

        [DisplayName("Absent 30 +")]
        public Int16 TeachersAbsent30Plus { get; set; }

        public virtual BaselineGeneral BaselineGenerals { get; set; }

    }
}