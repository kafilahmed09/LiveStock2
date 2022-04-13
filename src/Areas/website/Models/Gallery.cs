using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Gallery
    {
        [Key]
        public int GalleryID { get; set; }               
        public int GalleryFolderId { get; set; }               
        public string PicturePath { get; set; }                              
        public DateTime? CreatedOn { get; set; }
        public bool? Visibility { get; set; }
        public virtual GalleryFolder GalleryFolder { get; set; }
    }
}
