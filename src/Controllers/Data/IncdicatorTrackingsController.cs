using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Data;
using BES.Models.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using BES.Services.Profile;
using Microsoft.AspNetCore.Identity;

namespace BES.Controllers.Data
{
    public class IncdicatorTrackingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncdicatorTrackingsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

       
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return (usr.RegionalAccess);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        //public IncdicatorTrackingsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        // GET: IncdicatorTrackings
       //[Authorize(Roles = "Administrator,Education")]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.SectionID=id;
            if(id==926982)
            {
                ViewBag.Section = "Education Section";
            }
            else if(id==352769)
            {
                ViewBag.Section = "Development Section";
            }
            else
            {
                return RedirectToAction("index","BaselineGenerals" );
            }
            var applicationDbContext = _context.Schools.Where(a=>a.ProjectID==1 ).Include(a=>a.UC).Include(a=>a.UC.Tehsil).Include(a => a.UC.Tehsil.District).OrderBy(a => a.UC.Tehsil.District.RegionID).ThenBy(a => a.ClusterBEMIS).ThenBy(a => a.type); ;
            string ra = await GetCurrentUserId();
            int[] regions= ra.Split(',').Select(int.Parse).ToArray();
            //int[] array= Array.ConvertAll(ra, int.Parse);
            Console.WriteLine(regions);
            if(regions.Any())
            {
            //    applicationDbContext = from sch in _context.Schools
            //                           where regions.Contains(sch.UC.Tehsil.District.RegionID)
            //                           orderby sch.UC.Tehsil.District.RegionID,
                                       
            //                           ;
            }
            switch(regions.Length)
            {
                case 1:
                    applicationDbContext = applicationDbContext.Where(a => a.UC.Tehsil.District.RegionID == regions[0]).OrderBy(a => a.UC.Tehsil.District.RegionID).ThenBy(a => a.ClusterBEMIS).ThenBy(a => a.type); ;
                    break;
                case 2:
                    applicationDbContext = applicationDbContext.Where(a => a.UC.Tehsil.District.RegionID == regions[0] || a.UC.Tehsil.District.RegionID == regions[1]).OrderBy(a => a.UC.Tehsil.District.RegionID).ThenBy(a => a.ClusterBEMIS).ThenBy(a => a.type); ;
                    break;
                case 3:
                    applicationDbContext = applicationDbContext.Where(a => a.UC.Tehsil.District.RegionID == regions[0] || a.UC.Tehsil.District.RegionID == regions[1] || a.UC.Tehsil.District.RegionID == regions[2]).OrderBy(a => a.UC.Tehsil.District.RegionID).ThenBy(a => a.ClusterBEMIS).ThenBy(a => a.type); ;
                    break;
                case 4:
                    applicationDbContext = applicationDbContext.Where(a => a.UC.Tehsil.District.RegionID == regions[0] || a.UC.Tehsil.District.RegionID == regions[1] || a.UC.Tehsil.District.RegionID == regions[2] || a.UC.Tehsil.District.RegionID == regions[3]).OrderBy(a => a.UC.Tehsil.District.RegionID).ThenBy(a => a.ClusterBEMIS).ThenBy(a => a.type); ;
                    break;
                default: break;

            }
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: IncdicatorTrackings/Update
        public IActionResult Update(int id, int SecID)
        {
            int PId = SecID == 926982 ? 4 : 3;
            ViewBag.SecID = SecID;
            var sch = _context.Schools.Find(id);
            ViewBag.Sname = sch.SName;

            if (SecID == 926982)
            {
                ViewBag.Section = "Education Section";
            }
            else if (SecID == 352769)
            {
                ViewBag.Section = "Development Section";
            }
            var indiTrack =  _context.IncdicatorTracking.Where(a => a.SchoolID == id);
            var applicationDbContext = from Proj_Indicator in _context.Indicator
                                       join Proj_IncdicatorTracking in indiTrack on Proj_Indicator.IndicatorID equals Proj_IncdicatorTracking.IndicatorID into Proj_IncdicatorTracking_join
                                       from Proj_IncdicatorTracking in Proj_IncdicatorTracking_join.DefaultIfEmpty()
                                       where
                                         Proj_Indicator.PartnerID == PId
                                       // && (Proj_IncdicatorTracking.SchoolID == id ||
                                       //Proj_IncdicatorTracking.SchoolID == null)

                                       orderby
                                         Proj_Indicator.SequenceNo
                                       select new IndicatorTracking
                                       {
                                           IndicatorID = Proj_Indicator.IndicatorID,
                                           Indicator = Proj_Indicator.IndicatorName,
                                           isEvidence = Proj_Indicator.IsEvidenceRequire,
                                           ImageURL = Proj_IncdicatorTracking.ImageURL,
                                           DateOfUpload = Proj_IncdicatorTracking.DateOfUpload,
                                           SchoolID = id,
                                           IsUpload = Proj_IncdicatorTracking.IsUpload,
                                           TotalFilesUploaded = Proj_IncdicatorTracking.TotalFilesUploaded,
                                           isPotential = Proj_Indicator.IsPotential,
                                           isFeeder=Proj_Indicator.IsFeeder,
                                           isNextLevel=Proj_Indicator.IsNextLevel,
                                           EvidanceType= Proj_Indicator.EvidanceType
                                           //SchoolID = Proj_IncdicatorTracking.SchoolID == id ? id : Proj_IncdicatorTracking.SchoolID ==  null ? (int?)null : 0,

                                           // Proj_Indicator.SequenceNo
                                       };
            switch(sch.type)
            {
                case 1:
                    applicationDbContext = applicationDbContext.Where(a => a.isPotential == true);
                    break;
                case 2:
                    applicationDbContext = applicationDbContext.Where(a => a.isFeeder == true);
                    break;
                case 3:
                    applicationDbContext = applicationDbContext.Where(a => a.isNextLevel == true);
                    break;
            }
            //applicationDbContext = applicationDbContext.Where(a => a.SchoolID == id || a.SchoolID == null);
            //List<IndicatorTracking> indicatorTrackings = new List<IndicatorTracking>();
            //foreach(var indi in applicationDbContext)
            //{
            //    if(indi.SchoolID!=id ||)
            //}
            ViewData["ids"] = applicationDbContext.Select(a => a.IndicatorID).ToArray();
            return View(applicationDbContext.ToList());
        }
        public ActionResult Popup(int? id, int? iId)
        {
            ViewBag.PointOut = iId;

            string fpath = _context.IncdicatorTracking
                            .Where(a => a.SchoolID == id && a.IndicatorID == iId)
                            .Select(a => a.ImageURL).FirstOrDefault();

            var rootPath = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot"+fpath);
            DirectoryInfo dir = new DirectoryInfo(rootPath);
            FileInfo[] files = dir.GetFiles();
            ArrayList list = new ArrayList();
            List<string> filePaths = new List<string>();
            foreach (FileInfo file in files)
            {
                filePaths.Add(Path.GetFileName(file.FullName));
                //list.Add(file);
            }
            ViewData["filePaths"] = filePaths;
            ViewBag.fPath = fpath;
                return PartialView();
        }

        // POST: IncdicatorTrackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(int sID,int iID, DateTime EDate, int SecID)
        {
            // Save uploaded files
            var files = Request.Form.Files;

            string District = _context.Schools.Include(a => a.UC.Tehsil.District)
                                  .Where(a=>a.SchoolID==sID)
                                .Select(a => a.UC.Tehsil.District.DistrictName).FirstOrDefault();
            //string 
            var rootPath = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot\\Documents\\IndicatorEvidences\\");

            //string sPath = Path.Combine(rootPath + District + "/" + iID + "/", sID.ToString());
            string sPath = Path.Combine(rootPath + District + "/" + iID + "/");
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            short i = 1;
            string fileName = sID.ToString()+"-";
            foreach(var file in files)
            {
                string FullPathWithFileName = Path.Combine(sPath, fileName+i++ + Path.GetExtension(file.FileName));
                using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                
            }
             try
            {
                //create record
            IndicatorTracking IndiTrack = new IndicatorTracking();
            IndiTrack.IndicatorID = iID;
            IndiTrack.SchoolID = sID;
            IndiTrack.IsUpload = true;
            IndiTrack.TotalFilesUploaded = (short) files.Count;
            IndiTrack.DateOfUpload = EDate;
            IndiTrack.ImageURL= Path.Combine("/Documents/IndicatorEvidences/", District +  "//" + iID );//Server Path
            IndiTrack.CreateDate = DateTime.Now;
            IndiTrack.CreatedBy = User.Identity.Name;

            
            IndiTrack.Verified = false;

            _context.Add(IndiTrack);
           
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.Write(ex.InnerException.Message);
                return Json(new { success = false, responseText = ex.InnerException.Message });
            }
            //ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", incdicatorTracking.SchoolID);
            //return RedirectToAction(nameof(Update), new { id = sID, SecID = SecID });
            return Json(new { success = true, responseText = "Sucessfully Updated" }); //, sID = sID, SecID = SecID });
        }
        // GET: IncdicatorTrackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incdicatorTracking = await _context.IncdicatorTracking
                .Include(i => i.School)
                .FirstOrDefaultAsync(m => m.SchoolID == id);
            if (incdicatorTracking == null)
            {
                return NotFound();
            }

            return View(incdicatorTracking);
        }

        // GET: IncdicatorTrackings/Create
        public IActionResult Create()
        {
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName");
            return View();
        }

        // POST: IncdicatorTrackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IndicatorID,SchoolID,ImageURL,Verified,IsUpload,DateOfUpload,CreatedBy,CreateDate,UpdatedBy,UpdatedDate,VerifiedBy,VerifiedDate")] IndicatorTracking incdicatorTracking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incdicatorTracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", incdicatorTracking.SchoolID);
            return View(incdicatorTracking);
        }

        // GET: IncdicatorTrackings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incdicatorTracking = await _context.IncdicatorTracking.FindAsync(id);
            if (incdicatorTracking == null)
            {
                return NotFound();
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", incdicatorTracking.SchoolID);
            return View(incdicatorTracking);
        }

        // POST: IncdicatorTrackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IndicatorID,SchoolID,ImageURL,Verified,IsUpload,DateOfUpload,CreatedBy,CreateDate,UpdatedBy,UpdatedDate,VerifiedBy,VerifiedDate")] IndicatorTracking incdicatorTracking)
        {
            if (id != incdicatorTracking.SchoolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incdicatorTracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!IncdicatorTrackingExists(incdicatorTracking.SchoolID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", incdicatorTracking.SchoolID);
            return View(incdicatorTracking);
        }

        // GET: IncdicatorTrackings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incdicatorTracking = await _context.IncdicatorTracking
                .Include(i => i.School)
                .FirstOrDefaultAsync(m => m.SchoolID == id);
            if (incdicatorTracking == null)
            {
                return NotFound();
            }

            return View(incdicatorTracking);
        }

        // POST: IncdicatorTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incdicatorTracking = await _context.IncdicatorTracking.FindAsync(id);
            _context.IncdicatorTracking.Remove(incdicatorTracking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncdicatorTrackingExists(int id)
        {
            return _context.IncdicatorTracking.Any(e => e.SchoolID == id);
        }
    }
}
