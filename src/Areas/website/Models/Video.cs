using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Video
    {
        [Key]
        public int VideoID { get; set; }
        [Required]
        [DisplayName("Heading (Max Len: 80)")]
        [MaxLength(80)]
        public string Heading { get; set; }
        [MaxLength(2000)]
        [Required]
        [DisplayName("Description (Max Len: 2000)")]
        public string Description { get; set; }        
        public string PicturePath { get; set; }
        [Required]
        [DisplayName("Video Link")]
        public string VideoLink { get; set; }
        [Required]
        [RegularExpression("^[0-9]{2}:[0-9]{2}$", ErrorMessage = "Invalid duration format!")]
        [DisplayName("Duration: mm:ss")]
        public string Duration { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("On Date")]
        public DateTime OnDate { get; set; }        
        [DataType(DataType.Date)]        
        public DateTime? CreatedOn { get; set; }
        public bool Visibility { get; set; }
    }
}
