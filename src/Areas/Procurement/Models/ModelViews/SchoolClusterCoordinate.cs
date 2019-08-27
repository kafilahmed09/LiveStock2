using BES.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BES.Areas.Procurement.Models.ModelViews
{
    public class SchoolClusterCoordinate
    {
        [Key]
        public int PKEY { get; set; }
        public int SchoolID { get; set; }
        public string PSName { get; set; }
        public int? PBEMIS { get; set; }
        public string PX { get; set; }
        public string PY { get; set; }
        public string FSName { get; set; }
        public string FBEMIS { get; set; }
        public string FX { get; set; }
        public string FY { get; set; }
        public string NSName { get; set; }
        public string NBEMIS { get; set; }
        public string NX { get; set; }
        public string NY { get; set; }        

    }
}