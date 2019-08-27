using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models.ModelViews
{
    public class VReceivedItem
    {
        //public PPReceivedItem(int ActivityID
        //                        //int lotId, 
        //                        //int lotno, 
        //                        //int activityid, 
        //                        //short? PPAddendumTypeId,
        //                        //string attachment, 
        //                        //string RAttachment, 
        //                        //short contractorid, 
        //                        //string company, 
        //                        //string itemname, 
        //                        //string unit, 
        //                        //int IQuantity, 
        //                        //int FQuantity,
        //                        //int RQuantity,
        //                        //int addendum, 
        //                        //int estimatedunitrate, 
        //                        //int? actualunitrate, 
        //                        //DateTime actualdate, 
        //                        //DateTime? expirydate, 
        //                        //DateTime receivingdate, 
        //                        //short locationid,
        //                        //int GRNMID)
        //                        )
        //{

        //}
        [Key]
        public int LotItemId { get; set; }
        public int lotId { get; set; }
        [DisplayName("Lot-No")]
        public short lotno { get; set; }
        [DisplayName("Activity")]
        public short ActivityID { get; set; }
        public short? AddendumTypeId { get; set; }
        public string Attachment { get; set; }
        public short ContractorID { get; set; }
        public string CompanyName { get; set; }
        [DisplayName("Item")]
        public string ItemName { get; set; }
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
        public DateTime? ExpiryDate { get; set; }
        public int GRNMID { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public int RQuantity { get; set; }
        public string RAttachment { get; set; }
        public short? LocationId { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual BES.Models.Data.Contractor Contractor { get; set; }
        public virtual AddendumType AddendumType { get; set; }
        public virtual Location Location { get; set; }
        public virtual SCManagement SCManagement { get; set; }
    }
}