using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class UC
    {
        [Key]
        public int UCID { get; set; }

        [ForeignKey("Tehsil")]
        [Required]
        [DisplayName("Tehsil")]
        public int TehsilID { get; set; }

        [Required]
        [DisplayName("UC")]
        public string UCName { get; set; }

        [DisplayName("UC Code")]
        public string UCCode { get; set; }
        public virtual Tehsil Tehsil { get; set; }
    }
    // uc api name apiuc create  cood #1001
    public class ApiUC
    {
        public ApiUC(int tehsilID, int ucID, string ucname)
        {
            this.TehsilID = tehsilID;
            this.UcID = ucID;
            this.UcName = ucname;
        }
        [Key]
        public int UcID { get; set; }
        public int TehsilID { get; set; }
        public string UcName { get; set; }
    }
    // end #1001
}