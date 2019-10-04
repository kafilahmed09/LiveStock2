using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BES.Models.Reports
{
    public class BLEUDetailView
    {

        [Key]
        public short BLGeneralID { get; set; }
        public int Region { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string UCName { get; set; }
        public int? ClusterBEMISCode { get; set; }
        public int SchoolID { get; set; }
        public string SName { get; set; }
        public string SchoolType { get; set; }
        public string BEMISCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Level { get; set; }
        public int TotalEnrollment { get; set; }
        public int TotalEnrollGirls { get; set; }
        public int TotalEnrollBoys { get; set; }
        public int Enroll_Class_0 { get; set; }
        public int Enroll_Class_0_Girls { get; set; }
        public int Enroll_Class_0_Boys { get; set; }
        public int Enroll_Class_1 { get; set; }
        public int Enroll_Class_1_Girls { get; set; }
        public int Enroll_Class_1_Boys { get; set; }
        public int Enroll_Class_2 { get; set; }
        public int Enroll_Class_2_Girls { get; set; }
        public int Enroll_Class_2_Boys { get; set; }
        public int Enroll_Class_3 { get; set; }
        public int Enroll_Class_3_Girls { get; set; }
        public int Enroll_Class_3_Boys { get; set; }
        public int Enroll_Class_4 { get; set; }
        public int Enroll_Class_4_Girls { get; set; }
        public int Enroll_Class_4_Boys { get; set; }
        public int Enroll_Class_5 { get; set; }
        public int Enroll_Class_5_Girls { get; set; }
        public int Enroll_Class_5_Boys { get; set; }
        public int? Enroll_Class_6 { get; set; }
        public int? Enroll_Class_6_Girls { get; set; }
        public int? Enroll_Class_6_Boys { get; set; }
        public int? Enroll_Class_7 { get; set; }
        public int? Enroll_Class_7_Girls { get; set; }
        public int? Enroll_Class_7_Boys { get; set; }
        public int? Enroll_Class_8 { get; set; }
        public int? Enroll_Class_8_Girls { get; set; }
        public int? Enroll_Class_8_Boys { get; set; }
        public int? Enroll_Class_9 { get; set; }
        public int? Enroll_Class_9_Girls { get; set; }
        public int? Enroll_Class_9_Boys { get; set; }
        public int? Enroll_Class_10 { get; set; }
        public int? Enroll_Class_10_Girls { get; set; }
        public int? Enroll_Class_10_Boys { get; set; }
        public int TotalPresence { get; set; }
        public int TotalPresenceGrils { get; set; }
        public int TotalPresenceBoys { get; set; }
        public int Pres_Class_0 { get; set; }
        public int Pres_Class_0_Girls { get; set; }
        public int Pres_Class_0_Boys { get; set; }
        public int Pres_Class_1 { get; set; }
        public int Pres_Class_1_Girls { get; set; }
        public int Pres_Class_1_Boys { get; set; }
        public int Pres_Class_2 { get; set; }
        public int Pres_Class_2_Girls { get; set; }
        public int Pres_Class_2_Boys { get; set; }
        public int Pres_Class_3 { get; set; }
        public int Pres_Class_3_Girls { get; set; }
        public int Pres_Class_3_Boys { get; set; }
        public int Pres_Class_4 { get; set; }
        public int Pres_Class_4_Girls { get; set; }
        public int Pres_Class_4_Boys { get; set; }
        public int Pres_Class_5 { get; set; }
        public int Pres_Class_5_Girls { get; set; }
        public int Pres_Class_5_Boys { get; set; }
        public int? Pres_Class_6 { get; set; }
        public int? Pres_Class_6_Girls { get; set; }
        public int? Pres_Class_6_Boys { get; set; }
        public int? Pres_Class_7 { get; set; }
        public int? Pres_Class_7_Girls { get; set; }
        public int? Pres_Class_7_Boys { get; set; }
        public int? Pres_Class_8 { get; set; }
        public int? Pres_Class_8_Girls { get; set; }
        public int? Pres_Class_8_Boys { get; set; }
        public int? Pres_Class_9 { get; set; }
        public int? Pres_Class_9_Girls { get; set; }
        public int? Pres_Class_9_Boys { get; set; }
        public int? Pres_Class_10 { get; set; }
        public int? Pres_Class_10_Girls { get; set; }
        public int? Pres_Class_10_Boys { get; set; }
        public int TotalHeadcount { get; set; }
        public int TotalHeadcountGirls { get; set; }
        public int TotalHeadcountBoys { get; set; }
        public int HC_Class_0 { get; set; }
        public int HC_Class_0_Girls { get; set; }
        public int HC_Class_0_Boys { get; set; }
        public int HC_Class_1 { get; set; }
        public int HC_Class_1_Girls { get; set; }
        public int HC_Class_1_Boys { get; set; }
        public int HC_Class_2 { get; set; }
        public int HC_Class_2_Girls { get; set; }
        public int HC_Class_2_Boys { get; set; }
        public int HC_Class_3 { get; set; }
        public int HC_Class_3_Girls { get; set; }
        public int HC_Class_3_Boys { get; set; }
        public int HC_Class_4 { get; set; }
        public int HC_Class_4_Girls { get; set; }
        public int HC_Class_4_Boys { get; set; }
        public int HC_Class_5 { get; set; }
        public int HC_Class_5_Girls { get; set; }
        public int HC_Class_5_Boys { get; set; }
        public int? HC_Class_6 { get; set; }
        public int? HC_Class_6_Girls { get; set; }
        public int? HC_Class_6_Boys { get; set; }
        public int? HC_Class_7 { get; set; }
        public int? HC_Class_7_Girls { get; set; }
        public int? HC_Class_7_Boys { get; set; }
        public int? HC_Class_8 { get; set; }
        public int? HC_Class_8_Girls { get; set; }
        public int? HC_Class_8_Boys { get; set; }
        public int? HC_Class_9 { get; set; }
        public int? HC_Class_9_Girls { get; set; }
        public int? HC_Class_9_Boys { get; set; }
        public int? HC_Class_10 { get; set; }
        public int? HC_Class_10_Girls { get; set; }
        public int? HC_Class_10_Boys { get; set; }
        public int Sanctioned { get; set; }
        public int Attached { get; set; }
        public int Filled { get; set; }
        public int Sanc_JVT { get; set; }
        public int Filled_JVT { get; set; }
        public int Attach_JVT { get; set; }
        public int Sanc_JET { get; set; }
        public int Filled_JET { get; set; }
        public int Attach_JET { get; set; }
        public int Sanc_JAT { get; set; }
        public int Filled_JAT { get; set; }
        public int Attach_JAT { get; set; }
        public int Sanc_DM { get; set; }
        public int Filled_DM { get; set; }
        public int Attach_DM { get; set; }
        public int Sanc_SST_G { get; set; }
        public int Filled_SST_G { get; set; }
        public int Attach_SST_G { get; set; }
        public int Sanc_SST_S { get; set; }
        public int Filled_SST_S { get; set; }
        public int Attach_SST_S { get; set; }
        public int Sanc_MATH { get; set; }
        public int Filled_MATH { get; set; }
        public int Attach_MATH { get; set; }
        public int Sanc_PTI { get; set; }
        public int Filled_PTI { get; set; }
        public int Attach_PTI { get; set; }
        public string SchoolOwnLand { get; set; }
        public string CommunityDonateLand { get; set; }
        public string NeedAdditionalLand { get; set; }
        public string CommunityDonateAdditionalLand { get; set; }
        public string PTSMCFormed { get; set; }
        public short? PtsmcMembers { get; set; }
        public string PTSMCTrained { get; set; }
        public string PTSMCFunctional { get; set; }
        public string IfNotFunctional { get; set; }
        public short? RoomsTotal { get; set; }
        public short? RoomsFunctional { get; set; }
        public short? RoomsNonFunctional { get; set; }
        public short? RoomsNotInUsed { get; set; }
        public short? RoomsNeedRepair { get; set; }
        public short? ToiletsTotal { get; set; }
        public short? ToiletsFunctional { get; set; }
        public short? ToiletsNonFunctional { get; set; }
        public string ToiletsNonFuncReason { get; set; }
        public string WaterAvailable { get; set; }
        public string WaterSource { get; set; }
        public string IfWaterNotAvailble { get; set; }
        public string BoundryWall { get; set; }
        public string Gas { get; set; }
        public string Electricity { get; set; }
        public string Telephone { get; set; }
        public string SportsEquipment { get; set; }
        public string Library { get; set; }
        public string ITRoom { get; set; }
        public string ResourceHall { get; set; }
        public string ECERooms { get; set; }
        public short? StudentWithFurniture { get; set; }
        public short? TotalCupboards { get; set; }

        public int TypeSort { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }

    public class BLDashboardView
    {
        public short filter { get; set; }
        public List<BLEUDetailView> blV { get; set; }
    }
}
