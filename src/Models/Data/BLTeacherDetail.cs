using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLTeacherDetail
    {
         [Key]
        public int? BLTeacherID { get; set; }
        public short BLGeneralID { get; set; }
        
        [DisplayName("Teacher Name")]
        public string TeacherName { get; set; }
        public string Post { get; set; }
        public string Status { get; set; }
        public string Qualification { get; set; }
        public string Diploma { get; set; }
        public string MajorSubjects { get; set; }
        public bool TrainedOnECE { get; set; }

        public virtual BaselineGeneral baselinegeneral { get; set; }
    }
}