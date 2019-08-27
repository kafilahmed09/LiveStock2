using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Areas.Procurement.Models;
using BES.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BES.Areas.Procurement.Controllers
{
    [Area("Procurement")]
    public class ActivityDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/ActivityDetails
        public async Task<IActionResult> Index(short? id)
        {
            ViewBag.ActivityID = id;
            var activityDetail = _context.ActivityDetail.Include(p => p.Activity).Include(p => p.Step).Where(p => p.ActivityID == id);
            int totalSteps = _context.Step.Where(p => p.ProcurementPlanID == activityDetail.Max(a => a.Activity.ProcurementPlanID)).Count();
            if (totalSteps > 0 && activityDetail.Count() == totalSteps)
            {
                ViewBag.IsReachedLast = true;
            }
            else { ViewBag.IsReachedLast = false; }
            return PartialView(await activityDetail.ToListAsync());
        }

        // GET: Procurement/ActivityDetails/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityDetail = await _context.ActivityDetail
                .Include(a => a.Activity)
                .Include(a => a.Step)
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (activityDetail == null)
            {
                return NotFound();
            }

            return View(activityDetail);
        }

        // GET: Procurement/ActivityDetails/Create
        public IActionResult Create()
        {
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "Description");
            ViewData["StepID"] = new SelectList(_context.Step, "StepID", "StepID");
            return View();
        }

        // POST: Procurement/ActivityDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StepID,ActivityID,NotApplicable,PlannedDate,ActualDate,Attachment,CreatedDate,UpdatedBy")] ActivityDetail activityDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "Description", activityDetail.ActivityID);
            ViewData["StepID"] = new SelectList(_context.Step, "StepID", "StepID", activityDetail.StepID);
            return View(activityDetail);
        }

        // GET: Procurement/ActivityDetails/Edit/5
        public async Task<IActionResult> Edit(short ActivityID, short StepID)
        {
            if (ActivityID == 0 || StepID == 0)
            {
                return NotFound();
            }

            var activityDetail = await _context.ActivityDetail.Where(a=>a.ActivityID== ActivityID&& a.StepID==StepID).FirstOrDefaultAsync();
            activityDetail.Activity = _context.Activity.Find(ActivityID);
            activityDetail.Step = _context.Step.Find(StepID);
            ViewBag.PPName = _context.ProcurementPlan.Find(activityDetail.Step.ProcurementPlanID).Name;
            if (activityDetail == null)
            {
                return NotFound();
            }
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "Name", activityDetail.ActivityID);
            ViewData["StepID"] = new SelectList(_context.Step, "StepID", "Name", activityDetail.StepID);
            return View(activityDetail);
        }

        // POST: Procurement/ActivityDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short ActivityID, string PPName, int ActualCost, [Bind("StepID,ActivityID,NotApplicable,PlannedDate,ActualDate,Attachment,CreatedDate,UpdatedBy")] ActivityDetail activityDetail, IFormFile Attachment)
        {
            //if (id != activityDetail.ActivityID)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    if (activityDetail.NotApplicable == true)
                    {
                        activityDetail.Attachment = null;
                        activityDetail.ActualDate = null;
                        activityDetail.PlannedDate = null;
                    }
                    else
                    {
                        if (Attachment != null)
                            {
                            var rootPath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Procurement\\");
                            string fileName = Path.GetFileName(Attachment.FileName);
                            fileName = fileName.Replace("&", "n");
                            string AName = _context.Activity.Find(activityDetail.ActivityID).Name;
                            AName = AName.Replace("&", "n");
                            //var PPName = _context.ProcurementPlan.Find(activityDetail.Step.ProcurementPlanID).Name;
                            activityDetail.Attachment = Path.Combine(rootPath + PPName + "/" + "//" + AName + "//" + activityDetail.StepID + "//" + fileName);//Server Path                
                            //_context.ActivityDetail.Add(activityDetail);
                            string sPath = Path.Combine(rootPath + PPName + "/" + AName + "/", activityDetail.StepID.ToString());
                            if (!System.IO.Directory.Exists(sPath))
                            {
                                System.IO.Directory.CreateDirectory(sPath);
                            }
                            string FullPathWithFileName = Path.Combine(sPath, fileName);
                            using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                            {
                                await Attachment.CopyToAsync(stream);
                            }
                        }
                    }
                    Activity activityObj = _context.Activity.Find(activityDetail.ActivityID);
                    activityObj.ActualCost = ActualCost;                   
                    //_context.Update(activityObj);
                    _context.Update(activityDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityDetailExists(activityDetail.ActivityID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), "Activities", new { id = activityDetail.ActivityID });
            }
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "Description", activityDetail.ActivityID);
            ViewData["StepID"] = new SelectList(_context.Step, "StepID", "StepID", activityDetail.StepID);
            return View(activityDetail);
        }

        // GET: Procurement/ActivityDetails/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityDetail = await _context.ActivityDetail
                .Include(a => a.Activity)
                .Include(a => a.Step)
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (activityDetail == null)
            {
                return NotFound();
            }

            return View(activityDetail);
        }

        // POST: Procurement/ActivityDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var activityDetail = await _context.ActivityDetail.FindAsync(id);
            _context.ActivityDetail.Remove(activityDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityDetailExists(short id)
        {
            return _context.ActivityDetail.Any(e => e.ActivityID == id);
        }
    }
}
