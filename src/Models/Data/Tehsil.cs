using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class Tehsil
    {
        [Key]
        public int TehsilID { get; set; }

        [ForeignKey("District")]
        [Required]
        [DisplayName("District")]
        public int DistrictID { get; set; }

        [Required]
        [DisplayName("Tehsil")]
        public string TehsilName { get; set; }

        [DisplayName("Tehsil Code")]
        public string Tehsilcode { get; set; }

        public virtual District District { get; set; }
        public List<UC> UCList = new List<UC>();
    }

    //Api for Tehsil 1002
    public class ApiTehsil
    {
        public ApiTehsil()
        { }
        public ApiTehsil(int districtID, int tehsilID, string tehsilName)
        {
            this.DistrictID = districtID;
            this.TehsilID = tehsilID;
            this.TehsilName = tehsilName;
        }
        public int TehsilID { get; set; }


        [Required]
        [DisplayName("District")]
        public int DistrictID { get; set; }

        [Required]
        [DisplayName("Tehsil")]
        public string TehsilName { get; set; }
    }
    //Api 1002 end 
}