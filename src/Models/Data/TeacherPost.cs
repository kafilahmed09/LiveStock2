using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Models.Data
{
    [Table("TeacherPost", Schema = "Proj")]
    public class TeacherPost
    {
        [Key]
        public short PostID { get; set; }

        [DisplayName("Post")]
        public string PostName { get; set; }

        [DisplayName("Post Full name")]
        public string PostFullName { get; set; }
    }
}
