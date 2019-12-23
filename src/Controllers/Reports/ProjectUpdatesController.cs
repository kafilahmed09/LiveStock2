using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BES.Data;
using BES.Models.Data;
using BES.Models.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BES.Controllers.Reports
{
    public class ProjectUpdatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;

        public ProjectUpdatesController( ApplicationDbContext context)
        {
           // _userManager = userManager;
            _context = context;
        }

        //public ApplicationDbContext Context => _context;

        public IActionResult Index(int? RegionID, DateTime? StartDate, DateTime? EndDate)
        {
            List<Region> RegionList = new List<Region>();
            RegionList = _context.Regions.ToList();
            RegionList.Insert(0, new Region { RegionID = 0, RegionName = "All" });
            RegionList.RemoveAt(RegionList.Count - 1);
            ViewData["RegionID"] = new SelectList(RegionList, "RegionID", "RegionName");
            //var qry = _context.IncdicatorTracking.Include(a=>a.Indicator).Include(a=>a.School).i
            return View();
        }
        public JsonResult GetDistrict(int RegionID)
        {
            List<District> districtList = new List<District>();
            districtList = _context.Districts.Where(a => a.RegionID == RegionID).ToList();
            districtList.Insert(0, new District { DistrictID = 0, DistrictName = "All" });
            return Json(new SelectList(districtList, "DistrictID", "DistrictName"));
        }

        public async Task<IActionResult> FilterView(short? RID, short? DID, DateTime? StartDate, DateTime? EndDate)
        {
            var query = _context.IndicatorsSummaries.FromSql("exec IndicatorSummarySP @RID", new SqlParameter("@RID", RID == null ? (object)DBNull.Value : RID)).ToList<IndicatorsSummary>();
           // var q = context.blogs.FromSql("EXECUTE WID_Services_GetAll  @Id ", loggedInUser);
            //var query2 = from Regions in _context.Regions
            //            join Districts in _context.Districts on Regions.RegionID equals Districts.RegionID
            //            join Tehsils in _context.Tehsils on Districts.DistrictID equals Tehsils.DistrictID
            //            join Ucs in _context.UCs on Tehsils.TehsilID equals Ucs.TehsilID
            //            join Schools in _context.Schools on Ucs.UCID equals Schools.UCID
            //            join Proj_IncdicatorTracking in _context.IncdicatorTracking on Schools.SchoolID equals Proj_IncdicatorTracking.SchoolID
            //            where Proj_IncdicatorTracking.IsUpload==true && Proj_IncdicatorTracking.ReUpload==false
            //            select new
            //            {
            //                Regions.RegionID,
            //                Districts.DistrictID,
            //               // Proj_IncdicatorTracking.SchoolID,
            //                Proj_Indicator.IndicatorName,
            //                Proj_Indicator.IndicatorID,
            //               // Proj_IncdicatorTracking.IsUpload,
            //               // Proj_IncdicatorTracking.Verified,
            //                Proj_Indicator.IsPotential,
            //                Proj_Indicator.IsFeeder,
            //                Proj_Indicator.IsNextLevel,
            //                Schools.type,
            //               // potentail = Schools.
            //            };
            var schools = _context.Schools.Include(a=>a.UC.Tehsil.District) .Where(a => a.SchoolID > 0)
                        .Select(a => new { a.UC.Tehsil.District.RegionID,a.UC.Tehsil.District.DistrictID, a.SchoolID, a.type,a.NewConstruction,a.RepairRennovation });
            //if (DID != null)
            //{
            //    query = query.Where(a => a.DistrictID == DID);
            //    schools = schools.Where(a => a.DistrictID == DID);
            //}
            //else if (RID != null)
            //{
            //    query = query.Where(a => a.RegionID == RID);
            //    schools = schools.Where(a => a.RegionID == RID);
            //}
                var IndicatorList = await _context.Indicator.OrderBy(a => a.IndicatorID).Where(a=>a.IsSummary==true).ToListAsync();

            List<IndicatorsSummary> indicatorsSummary = new List<IndicatorsSummary>();
           

            foreach (var l in IndicatorList)
            {
                var IS = new IndicatorsSummary();
                var qq = query.Where(a => a.IndicatorID == l.IndicatorID);
                IS.IndicatorID = l.IndicatorID;
                //IS.PartnerID = l.PartnerID;
                //IS.IndicatorName = l.IndicatorName;

                //Total Target setting of Indicators 
                if (l.IndicatorID < 26)
                {
                    IS.PotentailTarget = l.IsPotential ? await schools.CountAsync(a => a.type == 1) : 0;
                    IS.FeederTarget = l.IsFeeder ? await schools.CountAsync(a => a.type == 2) : 0;
                    IS.NLTarget = l.IsNextLevel ? await schools.CountAsync(a => a.type == 3) : 0;
                }
                else if(l.IndicatorID<38) //New construction
                {
                    IS.PotentailTarget = l.IsPotential ? await schools.CountAsync(a => a.type == 1 && a.NewConstruction):0;
                    IS.FeederTarget = l.IsFeeder ? await schools.CountAsync(a => a.type == 2 && a.NewConstruction) : 0;
                    IS.NLTarget = l.IsNextLevel ? await schools.CountAsync(a => a.type == 3 && a.NewConstruction) : 0;
                }
                else //repair and renovation
                {
                    IS.PotentailTarget = l.IsPotential ? await schools.CountAsync(a => a.type == 1 && a.RepairRennovation) : 0;
                    IS.FeederTarget = l.IsFeeder ? await schools.CountAsync(a => a.type == 2 && a.RepairRennovation) : 0;
                    IS.NLTarget = l.IsNextLevel ? await schools.CountAsync(a => a.type == 3 && a.RepairRennovation) : 0;

                }
                IS.PotentailAchieve = qq.Sum(a=>a.PotentailAchieve);
               // IS.PotentailPercent = IS.PotentailTarget == 0 ? 0 : (IS.PotentailAchieve / IS.PotentailTarget);

                IS.FeederAchieve = qq.Sum(a => a.FeederAchieve);
                // IS.FeederPercent = IS.FeederPercent == 0 ? 0 : IS.FeederAchieve / IS.FeederTarget;

                IS.NLAchieve = qq.Sum(a => a.NLAchieve);
                //IS.NLPercent = IS.FeederPercent == 0 ? 0 : (IS.NLAchieve / IS.NLTarget);

                IS.TotalTarget = IS.PotentailTarget + IS.FeederTarget + IS.NLTarget;
                IS.TotalAchieve = IS.PotentailAchieve + IS.FeederAchieve + IS.NLAchieve;
                //IS.TotalPercent = IS.TotalAchieve / IS.TotalTarget;
                indicatorsSummary.Add(IS);
            }
            //var eduSummary = indicatorsSummary.Where(a => a.PartnerID == 4);
            //ViewBag.EduPercent = eduSummary.Sum(a => a.TotalAchieve)*100/eduSummary.Sum(a=>a.TotalTarget);

            //var DevSummary = indicatorsSummary.Where(a => a.PartnerID == 3);
            //ViewBag.DevPercent = DevSummary.Sum(a => a.TotalAchieve) * 100 / eduSummary.Sum(a => a.TotalTarget);

            return PartialView(indicatorsSummary);
        }

    }
}