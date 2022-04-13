using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LIVESTOCK.Models.Data
{
    public class Region
    {


        [Key]
        public int RegionID { get; set; }

        [Required]
        [DisplayName("Region")]
        public string RegionName { get; set; }

        [DisplayName("Region Code")]
        public string RegionCode { get; set; }

        [DisplayName("Total Districts")]
        public short  TotalDistricts { get; set; }

      
    }
}