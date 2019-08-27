using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Models.Data;
using BES.Data;

namespace BES.Controllers.Data
{
    public class BaselineGeneralsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaselineGeneralsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BaselineGenerals
        public IActionResult Index()
        {
            List<Region> RegionList = new List<Region>();
            RegionList = _context.Regions.ToList();
            RegionList.Insert(0, new Region { RegionID = 0, RegionName = "All" });
            RegionList.RemoveAt(RegionList.Count-1);
            ViewData["RegionID"] = new SelectList(RegionList, "RegionID", "RegionName");

            //var applicationDbContext = _context.BaselineGenerals.Include(b => b.School);
            return View();
        }
        public JsonResult GetDistrict(int RegionID)
        {
            List<District> districtList = new List<District>();
            districtList = _context.Districts.Where(a => a.RegionID == RegionID).ToList();
            districtList.Insert(0, new District { DistrictID = 0, DistrictName = "All" });
            return Json(new SelectList(districtList, "DistrictID", "DistrictName"));
        }

        public async Task<IActionResult> FilterView(short? RID, short? DID, short? TID, short? UID, short? LID)
        {
            var applicationDbContext = _context.BaselineGenerals.Include(s => s.School).Include(u=>u.School.UC).Include(t=>t.School.UC.Tehsil).Include(d=>d.School.UC.Tehsil.District)
                .Where(b=>b.School.SchoolOf==2);

            if (UID > 0)
                applicationDbContext = applicationDbContext.Where(q => q.School.UCID == UID);
            else if (TID > 0)
                applicationDbContext = applicationDbContext.Where(q => q.School.UC.TehsilID == TID);
            else if (DID > 0)
                applicationDbContext = applicationDbContext.Where(q => q.School.UC.Tehsil.DistrictID == DID);
            else if (RID > 0)
                applicationDbContext = applicationDbContext.Where(q => q.School.UC.Tehsil.District.RegionID == RID);

            applicationDbContext = applicationDbContext.OrderBy(a => a.School.UC.Tehsil.District.RegionID).ThenBy(a => a.ClusterBEMISCode).ThenBy(a => a.SchoolType);

            return PartialView(await applicationDbContext.ToListAsync());
        }

        // GET: BaselineGenerals/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baselineGeneral = await _context.BaselineGenerals
                .Include(b => b.School)
                .FirstOrDefaultAsync(m => m.BLGeneralID == id);
            if (baselineGeneral == null)
            {
                return NotFound();
            }

            return View(baselineGeneral);
        }

        // GET: BaselineGenerals/Create
        public IActionResult Create()
        {
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName");
            return View();
        }

        // POST: BaselineGenerals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BLGeneralID,UCID,SchoolID,SchoolType,ClusterBEMISCode,BEMISCode,SName,Type,Latitude,Longitude,VisitorName,Date,varified,VarifiedBy,Remarks")] BaselineGeneral baselineGeneral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baselineGeneral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", baselineGeneral.SchoolID);
            return View(baselineGeneral);
        }

        // GET: BaselineGenerals/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baselineGeneral = await _context.BaselineGenerals.FindAsync(id);

            ViewBag.NullableBool = new SelectList(new[]
                   {
                         new { Id = "", Name = "" },
                        new { Id = "True", Name = "Yes" },
                        new { Id = "False", Name = "No" }
                    }, "Id", "Name");
           // BaselineGeneral baselinegeneral = db.BaselineGenerals.Find(id);
            //if (baselineGeneral.School.UC.Tehsil.District.RegionID != loginUser.RegionID && !User.IsInRole("ICT"))
            //{
            //    return RedirectToAction("Login", "Account", new { area = "" });
            //}
            baselineGeneral.School = _context.Schools.Find(baselineGeneral.SchoolID);

            

            var modelCollection = new ModelCollection();
            modelCollection.baselineGeneral = baselineGeneral;
            modelCollection.blEnrollmentList = _context.BLEnrollments.Where(a => a.BaselineGenerals.BLGeneralID == id).ToList();
            modelCollection.blFacilitiesInfo = _context.BLFacilitiesInfoes.Where(a => a.BaselineGenerals.BLGeneralID == id).FirstOrDefault();
            modelCollection.blLandAvailable = _context.BLLandAvailables.Where(a => a.BaselineGeneral.BLGeneralID == id).FirstOrDefault();
            modelCollection.blPTSMCInfo = _context.BLPTSMCInfoes.Where(a => a.BaselineGenerals.BLGeneralID == id).FirstOrDefault();
            modelCollection.blTeacherSectionList = _context.BLTeacherSections.Where(a => a.BaselineGenerals.BLGeneralID == id).ToList();
            modelCollection.newSchool = _context.Schools.Where(a => a.SchoolID == baselineGeneral.SchoolID).FirstOrDefault();

            if (Convert.ToInt16(baselineGeneral.Type) > 1 && modelCollection.blEnrollmentList.Count() < 9)
            {
                modelCollection.blEnrollmentList.Add(new BLEnrollment());
                modelCollection.blEnrollmentList.Add(new BLEnrollment());
                modelCollection.blEnrollmentList.Add(new BLEnrollment());
            }
            if (Convert.ToInt16(baselineGeneral.Type) > 2 && modelCollection.blEnrollmentList.Count() < 11)
            {
                modelCollection.blEnrollmentList.Add(new BLEnrollment());
                modelCollection.blEnrollmentList.Add(new BLEnrollment());
            }

            ViewBag.UCID = new SelectList(_context.UCs.Where(a => a.TehsilID == _context.UCs.Where(b => b.UCID == baselineGeneral.UCID).Select(b => b.TehsilID).FirstOrDefault()), "UCID", "UCName", baselineGeneral.UCID);

            //modelCollection.baselineGeneral.BEMISCode = modelCollection.newSchool.BEMIS.ToString();
            //modelCollection.baselineGeneral.Latitude = modelCollection.newSchool.Latitude;
            //modelCollection.baselineGeneral.Longitude = modelCollection.newSchool.Longitude;
            //modelCollection.baselineGeneral.Type = modelCollection.newSchool.Level.ToString();
            //modelCollection.baselineGeneral.VarifiedBy = User.Identity.Name;
            //modelCollection.baselineGeneral.School.Latitude = modelCollection.newSchool.Latitude;
            //modelCollection.baselineGeneral.School.Longitude = modelCollection.newSchool.Longitude;
            //modelCollection.baselineGeneral.School.BEMIS = modelCollection.newSchool.BEMIS;
            return View(modelCollection);
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", baselineGeneral.SchoolID);
            return View(baselineGeneral);
        }

        // POST: BaselineGenerals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("BLGeneralID,UCID,SchoolID,SchoolType,ClusterBEMISCode,BEMISCode,SName,Type,Latitude,Longitude,VisitorName,Date,varified,VarifiedBy,Remarks")] BaselineGeneral baselineGeneral)
        {
            if (id != baselineGeneral.BLGeneralID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baselineGeneral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaselineGeneralExists(baselineGeneral.BLGeneralID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", baselineGeneral.SchoolID);
            return View(baselineGeneral);
        }

        // GET: BaselineGenerals/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baselineGeneral = await _context.BaselineGenerals
                .Include(b => b.School)
                .FirstOrDefaultAsync(m => m.BLGeneralID == id);
            if (baselineGeneral == null)
            {
                return NotFound();
            }

            return View(baselineGeneral);
        }

        // POST: BaselineGenerals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var baselineGeneral = await _context.BaselineGenerals.FindAsync(id);
            _context.BaselineGenerals.Remove(baselineGeneral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaselineGeneralExists(short id)
        {
            return _context.BaselineGenerals.Any(e => e.BLGeneralID == id);
        }
    }
}
