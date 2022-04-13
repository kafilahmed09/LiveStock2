using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeID { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }        
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime CreatedOn { get; set; }
    }
}
