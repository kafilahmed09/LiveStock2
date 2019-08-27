using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("LotItemImage", Schema = "Proc")]
    public class LotItemImage
    {
        [Key]
        public int ItemImageId { get; set; }
        public int LotItemId { get; set; }
        public string ImagePath { get; set; }
        public bool Visibility { get; set; }
        public virtual LotItem LotItem { get; set; }

    }
}