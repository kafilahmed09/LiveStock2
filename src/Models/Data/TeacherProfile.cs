using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Models.Data
{
    public class TeacherProfile
    {
        [Key]
        public int? TeacherID { get; set; }


        [DisplayName("School ID")]
        public int SchoolID { get; set; }

        [DisplayName("Post")]
        public short PostID { get; set; }
        [DisplayName("Teacher's Name")]
        public string TName { get; set; }

        [DisplayName("Father's Name")]
        public string FatherName { get; set; }

        [DisplayName("Husband's Name")]
        public string HusbandName { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "13 Digits without hash is required")]
        public string CNIC { get; set; }

        [DisplayName("Academic Qualification")]
        public string AcademicQualification { get; set; }

        [DisplayName("Professional Qualification")]
        public string ProfessionalQualification { get; set; }

        [DisplayName("Contract Award")]
        public bool? ContractAward { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Of Joining")]
        public DateTime? DateOfJoining { get; set; }
        //public string PhotoContract { get; set; }

        [DisplayName("Contact")]
        public string Contact { get; set; }

        [DisplayName("Bank Details")]
        public string BankDetail { get; set; }

        [DisplayName("Trained On PTSMC")]
        public bool? TrainedOnPTSMC { get; set; }

        [DisplayName("Trained On Domain")]
        public bool? TrainedOnDomain { get; set; }
        public string QRCodeFile { get; set; }
        public string EncryptedCode { get; set; }
        public string TeacherPicture { get; set; }
        public string CNICFront { get; set; }
        public string CNICBack { get; set; }
        public bool? DocAttested { get; set; }
        public short? NTSScore { get; set; }
        public short? MeritListRankNo { get; set; }
        public string ContractScanned { get; set; }

        public bool? IsBiometricReg { get; set; }

        public string BioLeftIndex { get; set; }
        public string BioLeftThumb { get; set; }
        public string BioRightIndex { get; set; }
        public string BioRighThumb { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Registeration Date")]
        public DateTime? BiometricRegDate { get; set; }
        public virtual School School { get; set; }
        public virtual TeacherPost TeacherPost { get; set; }
    }
}
