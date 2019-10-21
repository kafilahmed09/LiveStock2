using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Models.Data
{
    [Table("TeacherProfile", Schema = "Proj")]
    public class TeacherProfile
    {
        [Key]
        public int? TeacherID { get; set; }


        [DisplayName("School ID")]
        public int SchoolID { get; set; }
        [Required]
        [DisplayName("Post")]
        public short PostID { get; set; }
        [Required]
        [DisplayName("Teacher's Name")]
        public string TName { get; set; }
        [Required]
        [DisplayName("Husband/Father Name")]
        public string FatherHusbandName { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "13 Digits without hash is required")]
        public string CNIC { get; set; }

        public bool IsFemale { get; set; }
        public bool IsProjectTeacher { get; set; }

        [DisplayName("Contract Award")]
        public bool? ContractAward { get; set; }

        public string ContractUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Joining")]
        public DateTime? DateOfJoining { get; set; }
        //public string PhotoContract { get; set; }

        public DateTime ContractDate { get; set; }


        [NotMapped]
        public string QRCodeFile { get; set; }
        [NotMapped]
        public string EncryptedCode { get; set; }
        [DisplayName("ECE Pre test Score")]
        public int? EcePreScore { get; set; }
        [DisplayName("ECE Post test Score")]
        public int? EcePostScore { get; set; }
        [DisplayName("EST Pre test Score")]
        public int? EstPreScore { get; set; }
        [DisplayName("EST Post test Score")]
        public int? EstPostScore { get; set; }
        [DisplayName("HT Pre test Score")]
        public int? HTPreScore { get; set; }
        [DisplayName("HT Post test Score")]
        public int? HTPostScore { get; set; }

        [NotMapped]
        public bool? IsBiometricReg { get; set; }
        [NotMapped]
        public string BioLeftIndex { get; set; }
        [NotMapped] public string BioLeftThumb { get; set; }
        [NotMapped] public string BioRightIndex { get; set; }
        [NotMapped] public string BioRighThumb { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Registeration Date")]
        [NotMapped] public DateTime? BiometricRegDate { get; set; }
        public virtual School School { get; set; }
        public virtual TeacherPost TeacherPost { get; set; }
    }
}
