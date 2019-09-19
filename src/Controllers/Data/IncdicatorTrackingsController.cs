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

namespace BES.Controllers.Data
{
    public class IncdicatorTrackingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncdicatorTrackingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncdicatorTrackings
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
            var applicationDbContext = _context.Schools.Where(a=>a.SchoolOf==2).Include(a=>a.UC).Include(a=>a.UC.Tehsil).Include(a => a.UC.Tehsil.District);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: IncdicatorTrackings/Update
        public IActionResult Update(int id, int SecID)
        {
            int PId = SecID == 926982 ? 4 : 3;

            var applicationDbContext = from Proj_Indicator in _context.Indicator
                                       join Proj_IncdicatorTracking in _context.IncdicatorTracking on Proj_Indicator.IndicatorID equals Proj_IncdicatorTracking.IndicatorID into Proj_IncdicatorTracking_join
                                       from Proj_IncdicatorTracking in Proj_IncdicatorTracking_join.DefaultIfEmpty()
                                       where
                                         Proj_Indicator.PartnerID==PId
                                       orderby
                                         Proj_Indicator.SequenceNo
                                       select new IncdicatorTracking
                                       {
                                           IndicatorID = Proj_Indicator.IndicatorID,
                                           IndicatorName= Proj_Indicator.IndicatorName,
                                           isEvidence= Proj_Indicator.IsEvidenceRequire,
                                           ImageURL = Proj_IncdicatorTracking.ImageURL,
                                           DateOfUpload = Proj_IncdicatorTracking.DateOfUpload,
                                           SchoolID = id,
                                          // Proj_Indicator.SequenceNo
                                       };

            return View(applicationDbContext.ToList());
        }

        // POST: IncdicatorTrackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("IndicatorID,SchoolID,ImageURL,Verified,IsUpload,DateOfUpload,CreatedBy,CreateDate,UpdatedBy,UpdatedDate,VerifiedBy,VerifiedDate")] IncdicatorTracking incdicatorTracking, IFormFile Attachment)
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
        public async Task<IActionResult> Create([Bind("IndicatorID,SchoolID,ImageURL,Verified,IsUpload,DateOfUpload,CreatedBy,CreateDate,UpdatedBy,UpdatedDate,VerifiedBy,VerifiedDate")] IncdicatorTracking incdicatorTracking)
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
        public async Task<IActionResult> Edit(int id, [Bind("IndicatorID,SchoolID,ImageURL,Verified,IsUpload,DateOfUpload,CreatedBy,CreateDate,UpdatedBy,UpdatedDate,VerifiedBy,VerifiedDate")] IncdicatorTracking incdicatorTracking)
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
