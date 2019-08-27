using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLFeederDetail
    {
        [Key]
        public Int16 BLFeederDetailID { get; set; }
        public Int16 BLFeederSchoolID { get; set; }

        [DisplayName("Class")]
        public Int16 ClassID { get; set; }

        [DisplayName("Total Boys")]
        public Int16 TotalBoys { get; set; }

        [DisplayName("Total Girls")]
        public Int16 TotalGirls { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
        public virtual BLFeederSchool BlFeederSchool { get; set; }
    }
}