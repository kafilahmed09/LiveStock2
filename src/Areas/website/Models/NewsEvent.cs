using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class NewsEvent
    {
        [Key]
        public int NewsEventID { get; set; }
        [Required]
        [DisplayName("Heading (Max Len: 300)")]
        [MaxLength(300)]
        public string Heading { get; set; }
        [MaxLength(2000)]
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }           
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("On Date")]
        public DateTime OnDate { get; set; }        
        [DataType(DataType.Date)]        
        public DateTime? CreatedOn { get; set; }
        public bool Visibility { get; set; }
    }
}
