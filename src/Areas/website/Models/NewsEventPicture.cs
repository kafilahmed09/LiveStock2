using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class NewsEventPicture
    {
        [Key]
        public int NewsEventPictureID { get; set; }
        [Required]
        public string PicturePath { get; set; }
        public int NewsEventID { get; set; }
        public virtual NewsEvent NewsEvent { get; set; }
    }
}
