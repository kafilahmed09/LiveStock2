using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("AddendumDetail", Schema = "Proc")]
    public class AddendumDetail
    {
        [Key]
        public int AddendumDetailId { get; set; }
        public int AddendumId { get; set; }
        public int LotItemId { get; set; }
        public int Quantity { get; set; }
        public virtual Addendum Addendum { get; set; }
        public virtual LotItem LotItem { get; set; }
    }
}