using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LIVESTOCK.Areas.website.Models
{
    public class Director
    {
        [Key]
        public int DirectorID { get; set; }
        [Required]
        [DisplayName("Name")]
        public string HeadName { get; set; }
        [DisplayName("Under Section")]
        public string Department { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Date)]                
        public DateTime? CreatedOn { get; set; }
    }
}
