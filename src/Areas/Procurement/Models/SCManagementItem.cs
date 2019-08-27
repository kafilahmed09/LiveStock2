using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("SCManagementItem", Schema = "Proc")]
    public class SCManagementItem
    {
        public SCManagementItem()
        {            
        }
        public SCManagementItem(int ItemId)
        {
            this.LotItemId = ItemId;
        }
        public SCManagementItem(int ParentId, int ItemId, int Qty)
        {
            this.SCManagementID = ParentId;
            this.LotItemId = ItemId;
            this.Quantity = Qty;
        }
        [Key]
        public int SCManagementItemID { get; set; }

        [DisplayName("GRN")]
        public int SCManagementID { get; set; }
        public int LotItemId { get; set; }
        public int FQuantity { get; set; }
        public int ActualUnitRate { get; set; }
        public int Quantity { get; set; }        
        public virtual SCManagement SCManagement { get; set; }
        public virtual LotItem LotItem { get; set; }
   }
    public class ItemReceivedDetail
    {
        public List<SCManagementItem> ItemReceivedDetails { get; set; }
    }
}