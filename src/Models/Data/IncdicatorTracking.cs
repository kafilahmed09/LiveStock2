using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Models.Data
{
    public class IncdicatorTracking
    {
        [Key, Column(Order = 0)]
        public int IndicatorID { get; set; }
        [Key, Column(Order = 1)]
        public int SchoolID { get; set; }
        public string ImageURL { get; set; }
        public bool Verified { get; set; }
        public bool IsUpload { get; set; }
        public DateTime DateOfUpload { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }

        public virtual School School { get; set; }

    }
}
