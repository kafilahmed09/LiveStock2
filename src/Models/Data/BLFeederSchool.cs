using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLFeederSchool
    {
         [Key]
        public Int16 BLFeederSchoolID { get; set; }
        public Int16 BLGeneralID { get; set; }

        [DisplayName("School Name")]
        public string SchoolName { get; set; }

        [DisplayName("School Level")]
        public string SchoolLevel { get; set; }

        [DisplayName("BEMIS Code")]
        public string BEMISCode { get; set; }

        [DisplayName("Latitude")]
        public string Latitude { get; set; }

        [DisplayName("Longitude")]
        public string Longitude { get; set; }

        public virtual BaselineGeneral BaselineGenerals { get; set; }
    }
}