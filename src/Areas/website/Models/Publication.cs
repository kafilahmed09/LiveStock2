using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Publication
    {
        [Key]
        public int PublicationID { get; set; }
        [Required]
        [Display(Name = "Publication Title")]
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool Visibility { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
    }
}
