using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Initiative
    {
        [Key]
        public int InitiativeId { get; set; }
        [Required]              
        public string Name { get; set; }          
        public string filepath { get; set; }        
        [Required]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }
    }
}
