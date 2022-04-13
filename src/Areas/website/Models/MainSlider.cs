using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class MainSlider
    {
        [Key]
        public int MainSliderID { get; set; }
        [MaxLength(25)]        
        [DisplayName("Title (Max Len: 25, Optional)")]
        public string Title { get; set; }                              
        public string PicturePath { get; set; }
        [DisplayName("Order Sequence")]
        public int OrderSequence { get; set; }                              
        public DateTime? CreatedOn { get; set; }
        public bool? Visibility { get; set; }
    }
}
