using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class GalleryFolder
    {
        [Key]
        public int GalleryFolderId { get; set; }
        public string FolderTitle { get; set; }
        public string FolderImagePath { get; set; }
        public int ImageCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Visibility { get; set; }
    }
}
