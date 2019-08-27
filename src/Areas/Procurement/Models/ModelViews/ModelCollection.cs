using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models.ModelViews
{
    public class ModelCollection
    {        
        public SCManagement SCMObj { get; set; }
        public ItemReceivedDetail ItemReceivedDetails { get; set; }
        public VLotItemDetail ItemDetails { get; set; }

    }
}