using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BES.Models.Data
{
    public class BLFacilitiesInfo
    {
        [Key]
        public Int16 BLFacilitiesInfoID { get; set; }
        public Int16 BLGeneralID { get; set; }

        [DisplayName("Rooms Total")]
        public Int16 RoomsTotal { get; set; }

        [DisplayName("Rooms Func")]
        public Int16? RoomsFunctional { get; set; }

        [DisplayName("Rooms Non Func")]
        public Int16? RoomsNonFunctional { get; set; }

        [DisplayName("Rooms Not in use")]
        public Int16? RoomsNotInUsed { get; set; }

        [DisplayName("Rooms Need Repair")]
        public Int16? RoomsNeedRepair { get; set; }

        [DisplayName("Toilets Total")]
        public Int16 ToiletsTotal { get; set; }

        [DisplayName("Toilets Func")]
        public Int16? ToiletsFunctional { get; set; }

        [DisplayName("Toilets Non Func")]
        public Int16? ToiletsNonFunctional { get; set; }

        [DisplayName("Toilets Non Func Reason")]
        public string ToiletsNonFuncReason { get; set; }

        [DisplayName(" Water Available")]
        public bool IsWaterAvailable { get; set; }

        [DisplayName(" Water Source")]
        public string WaterSource { get; set; }

        [DisplayName("Water Reason")]
        public string IfWaterNotAvailble { get; set; }

        [DisplayName("Boundry wall")]
        public bool IsBoundrywall { get; set; }

        [DisplayName("Gas")]
        public bool IsGas { get; set; }

        [DisplayName("Telephone")]
        public bool IsTelephone { get; set; }

        [DisplayName("Electricity")]
        public bool IsElectricity { get; set; }

        //[DisplayName("Space For Extension")]
       // public bool IsSpaceForExtension { get; set; }

        [DisplayName("Sports Equipment")]
        public bool IsSportsEquipment { get; set; }

        [DisplayName("Library")]
        public bool IsLibrary { get; set; }

        [DisplayName("IT Room")]
        public bool IsITRoom { get; set; }

        [DisplayName("Resource Hall")]
        public bool IsResourceHall { get; set; }

        [DisplayName("ECE Rooms")]
        public bool IsECERooms { get; set; }

        //[DisplayName("Trainning Materials ")]
        //public bool IsTrainningMaterials { get; set; }

        [DisplayName("Student With Furniture")]
        public Int16 StudentWithFurniture { get; set; }
        [DisplayName("Total Cupboards")]
        public Int16? TotalCupboards { get; set; }

        public virtual BaselineGeneral BaselineGenerals { get; set; }
    }
}