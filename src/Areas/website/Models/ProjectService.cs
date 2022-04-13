using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class ProjectService
    {
        [Key]
        public int ServiceID { get; set; }
        public int ServiceTypeID { get; set; }
        [DisplayName("S.No")]
        public int SerialNo { get; set; }
        [Required]
        [DisplayName("Name of Service")]
        public string Name { get; set; }
        public bool Visibility { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime CreatedOn { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
