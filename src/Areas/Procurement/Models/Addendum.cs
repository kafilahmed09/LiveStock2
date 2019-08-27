using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Addendum", Schema = "Proc")]
    public class Addendum
    {
        [Key]
        public int AddendumId { get; set; }
        public int LotId { get; set; }
        public short AddendumTypeId { get; set; }
        [DisplayName("Contract Agreement")]
        public string Attachment { get; set; }

        [DisplayName("Remarks(If Any)")]
        public string Remarks { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual AddendumType AddendumType { get; set; }
    }
}