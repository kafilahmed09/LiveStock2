using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLLandAvailable
    {
         [Key]
        public short LandAvailableID { get; set; }

        [DisplayName("School Name")]
         public short BLGeneralID { get; set; }

        [DisplayName("School Own Land")]
        public bool SchoolOwnLand { get; set; }

        [DisplayName("Community Donating Land")]
        public bool? CommunityDonateLand { get; set; }

        [DisplayName("Require Additional Land")]
        public bool? NeedAdditionalLand { get; set; }

        [DisplayName("Community Donating Additional Land")]
        public bool? CommunityDonateAdditionalLand { get; set; }

        public virtual BaselineGeneral BaselineGeneral { get; set; }
    }
}