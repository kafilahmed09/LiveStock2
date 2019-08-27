using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BES.Areas.Procurement.Models
{
    [Table("ProcurementPlan", Schema = "Proc")]
    public class ProcurementPlan
    {
        [Key]
        public short ProcurementPlanID { get; set; }
        [DisplayName("Procurement Head")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}