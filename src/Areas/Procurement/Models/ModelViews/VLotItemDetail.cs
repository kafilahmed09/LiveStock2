using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models.ModelViews
{
    public class VLotItemDetail
    {
        [Key]
        public int LotItemId { get; set; }
        public int lotId { get; set; }
        [DisplayName("Lot-No")]
        public short lotno { get; set; }
        [DisplayName("Activity")]
        public short ActivityID { get; set; }
        [DisplayName("Addendum Type")]
        public short? AddendumTypeId { get; set; }
        public string Attachment { get; set; }
        public short ContractorID { get; set; }
        public string CompanyName { get; set; }
        [DisplayName("Item")]
        public string ItemName { get; set; }
        [DisplayName("Unit")]
        public string Unit { get; set; }
        [DisplayName("Initial Quantity")]
        public int IQuantity { get; set; }
        [DisplayName("Final Quantity")]
        public int FQuantity { get; set; }
        [DisplayName("Addandum Call")]
        public int Addandum { get; set; }
        [DisplayName("Estimated Unit Rate")]
        public int EstimatedUnitRate { get; set; }
        [DisplayName("Actual Unit Rate")]
        public int? ActualUnitRate { get; set; }
        [DisplayName("Expiry")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActualDate { get; set; }
        [DisplayName("Expiry")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LExpiryDate { get; set; }
        public DateTime? AExpiryDate { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual Activity Activity { get; set; }        
        public virtual BES.Models.Data.Contractor Contractor { get; set; }
        public virtual AddendumType AddendumType { get; set; }
    }
}