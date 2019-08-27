using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLEnrollment
    {
        [Key]
        public Int16 BLEnrollmentID {get; set;}
        public Int16 BLGeneralID {get; set;}

        [Required]
        [DisplayName("Class")]
        public Int16 ClassID{get; set;}

        [DisplayName("Enroll Boys")]
        public Int16 EnrollBoys{get; set;}

        [DisplayName("Enroll Girls")]
        public Int16 EnrollGirls{get; set;}

        [DisplayName("Presence Boys")]
        public Int16 PresenceBoys{get; set;}

        [DisplayName("Presence Girls")]
        public Int16 PresenceGirls{get; set;}

        [DisplayName("Head Count Boys")]
        public Int16 HeadCountBoys{get; set;}

        [DisplayName("Head Count Girls")]
        public Int16 HeadCountGirls { get; set; }

        public virtual BaselineGeneral BaselineGenerals { get; set; }

        public virtual SchoolClass schoolClass { get; set; }

    }
}