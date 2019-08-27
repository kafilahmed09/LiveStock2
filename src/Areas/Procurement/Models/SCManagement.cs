using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("SCManagement", Schema = "Proc")]
    public class SCManagement
    {
        [Key]
        public int SCManagementID { get; set; }

        [DisplayName("Lot")]
        public int LotId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]       
        [DisplayName("Received Date")]
        public DateTime ReceivingDate { get; set; }
        [DisplayName("Location")]
        public short LocationId { get; set; }
        [DisplayName("Attachment")]
        public string Attachment { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual Location Location { get; set; }

    }
}