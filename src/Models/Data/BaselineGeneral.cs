using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BaselineGeneral
    {
        [Key]
        public Int16 BLGeneralID { get; set; }
        public int? UCID { get; set; }
        [Required]
        [DisplayName("School Name")]
        public int SchoolID { get; set; }
        [DisplayName("School Type")]
        public int SchoolType { get; set; }
        [Required]
        [DisplayName("Cluster BEMIS Code")]
        public int? ClusterBEMISCode { get; set; }

        [DisplayName("BEMIS Code")]
        public string BEMISCode { get; set; }
        [DisplayName("School Name")]
        public string SName { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

       public string Latitude { get; set; }

       public string Longitude { get; set; }

       [DisplayName("Visitor")]
       public string VisitorName { get; set; }
              
        [DisplayName("Date")]
       [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "Please tick the check box, in order to verify the inforamtion")]
        [DisplayName("M&E Verified")]
        public bool varified { get; set; }

        [DisplayName("Verified By")]
        public string VarifiedBy { get; set; }
        public string Remarks { get; set; }
        public virtual School School { get; set; }
    }
}