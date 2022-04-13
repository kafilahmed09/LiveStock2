using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class ServiceCenter
    {
        [Key]
        public int ServiceCenterID { get; set; }
        [DisplayName("S.No")]
        public int SerialNo { get; set; }
        [Required]
        [DisplayName("Service Center")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Numbers { get; set; }
        [Required]
        public bool Visibility { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime CreatedOn { get; set; }
    }
}
