using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models
{
    [Table("LotItem", Schema = "Proc")]
    public class LotItem
    {
        public LotItem(int lotid, string itemname, short Unit, int fquantity, int rquantity, int actualunitrate)
        {
            this.lotId = lotid;
            this.ItemName = itemname;
            this.UnitId = Unit;
            this.Quantity = fquantity;
            this.EstimatedUnitRate = rquantity;
            this.ActualUnitRate = actualunitrate;
        }
        public LotItem()
        {

        }
        [Key]
        public int LotItemId { get; set; }
        public int lotId { get; set; }

        [DisplayName("Name")]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Estimated Unit Rate")]
        public int EstimatedUnitRate { get; set; }
        [DisplayName("Actual Unit Rate")]
        public int? ActualUnitRate { get; set; }
        public short UnitId { get; set; }
        public string Description { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual Unit Unit { get; set; }
    }
    public class ItemDetail
    {
        public List<LotItem> ItemDetails { get; set; }
    }
}