using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LIVESTOCK.Models.Data
{
    public class District
    {

        [Key]
        public int DistrictID { get; set; }

        [ForeignKey("Region")]
        [Required]
        [DisplayName("Region")]
        public int RegionID { get; set; }

        [Required]
        [DisplayName("District")]
        public string DistrictName { get; set; }

        [DisplayName("District Code")]
        public string DistrictCode { get; set; }

       

        // public List<Tehsil> TehsilList = new List<Tehsil>();
        public virtual Region Region { get; set; }
       
    }
}