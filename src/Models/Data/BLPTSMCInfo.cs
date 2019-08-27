using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLPTSMCInfo
    {
        [Key]
        public Int16 BLPtsmcID { get; set; }
        public Int16 BLGeneralID { get; set; }

        [DisplayName(" PTSMC Formed")]
        public bool IsPtsmcFormed { get; set; }

        [DisplayName(" PTSMC Members")]
        public Int16? PtsmcMembers { get; set; }

        [DisplayName(" PTSMC Trained")]
        public bool? IsPtsmcTrained { get; set; }

        [DisplayName(" PTSMC Functional")]
        public bool? IsPtsmcFunctional { get; set; }

        [DisplayName(" Reason of Non Functional")]
        public string IfNotFunctional { get; set; }

        public virtual BaselineGeneral BaselineGenerals { get; set; }
    }
}