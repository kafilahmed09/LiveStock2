using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class ModelCollection
    {
        public ModelCollection()
        {
            baselineGeneral = new BaselineGeneral();
        }
        public School newSchool { get; set; }
        public BaselineGeneral baselineGeneral { get; set; }
        public List<BLEnrollment> blEnrollmentList { get; set; }
        public List<BLTeacherSection> blTeacherSectionList { get; set; }
        public BLLandAvailable blLandAvailable { get; set; }
        public BLPTSMCInfo blPTSMCInfo { get; set; }
        public BLFacilitiesInfo blFacilitiesInfo { get; set; }
        public int UCID { get; set; }
    }
}