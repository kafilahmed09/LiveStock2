using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LIVESTOCK.Areas.website.Models
{
    public class Budget
    {
        [Key]
        public int BudgetID { get; set; }
        [Required]
        [DisplayName("Year")]        
        public int Year { get; set; }
        [Required]        
        public int Quarter { get; set; }        
        public string filepath { get; set; }
        public Int64 Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }
    }
}
