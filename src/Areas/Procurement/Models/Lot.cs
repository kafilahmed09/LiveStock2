using BES.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("Lot", Schema = "Proc")]
    public class Lot
    {
        [Key]
        public int lotId { get; set; }
        [DisplayName("Lot.No")]
        public Int16 lotno { get; set; }
        [DisplayName("Total Items in Lot")]
        public short ItemTotal { get; set; }
        [DisplayName("Description")]
        public string lotDescription { get; set; }
        [DisplayName("Contract Agreement")]
        public string Attachment { get; set; }
        public Int16 ActivityID { get; set; }
        [DisplayName("Contractor/Supplier")]
        public Int16? ContractorID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? ExpiryDate { get; set; }
        public int? ActualCost { get; set; }
        public bool IsMatched { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual BES.Models.Data.Contractor Contractor { get; set; }
    }

    public class LotItemDetail
    {
        [Key]
        public int lotId { get; set; }
        [DisplayName("Lot.No")]
        public Int16 lotno { get; set; }
        [DisplayName("Total Items in Lot")]
        public short ItemTotal { get; set; }
        [DisplayName("Description")]
        public string lotDescription { get; set; }
        [DisplayName("Contract Agreement")]
        public string Attachment { get; set; }
        public Int16 ActivityID { get; set; }
        [DisplayName("Contractor/Supplier")]
        public Int16? ContractorID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        public int? ActualCost { get; set; }
        public bool IsMatched { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Activity Activity { get; set; }
        public List<LotItem> Items { get; set; }
    }
}
