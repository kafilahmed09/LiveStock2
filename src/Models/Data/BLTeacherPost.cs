using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLTeacherPost
    {
        [Key]
        public Int16 BLTeacherPostID { get; set; }

        [Required]
        [DisplayName("Post Name")]
        public string PostName { get; set; }

    }
}