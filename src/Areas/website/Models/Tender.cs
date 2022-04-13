using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Tender
    {
        [Key]
        public int TenderID { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool Visibility { get; set; }
        public bool Status { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Open Date")]
        public DateTime OpenDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Close Date")]
        public DateTime CloseDate { get; set; }        
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }
    }
}
