using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLTeacherSection
    {
        [Key]
        public int BLTeacherSectionID{ get; set; }
        public Int16 BLGeneralID { get; set; }
        
        [Required]
        [DisplayName("Post Name")]
        public Int16 BLTeacherPostID{ get; set; }
        public Int16 Sanctioned{ get; set; }
        public Int16 Filled{ get; set; }
        public Int16 Attached { get; set; }

        public virtual BLTeacherPost BLTeacherPost { get; set; }
        public virtual BaselineGeneral BaselineGenerals { get; set; }

    }
}