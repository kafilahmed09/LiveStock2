using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class ContractorType
    {
        [Key]
        public Int16 ContractorTypeID { get; set; }    
        public string ContractorTypeName { get; set; }
    }
}