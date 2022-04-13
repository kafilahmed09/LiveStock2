using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class ImportantLink
    {
        [Key]
        public short ImportantLinkID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
