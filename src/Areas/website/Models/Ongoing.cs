using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Ongoing
    {
        [Key]
        public int OngoingID { get; set; }
        [DisplayName("S.No")]
        public int SerialNo { get; set; }
        [Required]
        [DisplayName("Project ID")]
        public string ProjectID { get; set; }
        [DisplayName("PSDP#")]
        public string PSDP { get; set; }
        [Required]
        [DisplayName("Project Name")]
        public string Name { get; set; }
        [DisplayName("Estimated Cost")]
        public double EstimatedCost { get; set; }
        [DisplayName("Exp Upto June 2021")]
        public double Exp { get; set; }
        [DisplayName("Fin Ach%")]
        public double Fin { get; set; }
        [DisplayName("Allocation 2021-2022")]
        public double Allocation { get; set; }
        [DisplayName("Fin Tar%")]
        public double FinTar { get; set; }
        [DisplayName("Thr: Fwd:")]
        public double Thr { get; set; }        
        [DisplayName("Is Ongoing Project")]
        public int Status { get; set; }              
        public int DirectorID { get; set; }        
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime? CreatedOn { get; set; }

        public virtual Director Director { get; set; }
    }
}
