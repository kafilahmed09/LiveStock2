using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Areas.Procurement.Models;
using BES.Data;
using BES.Models.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BES.Areas.Procurement.Controllers
{
    [Area("Procurement")]
    public class LotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotsController(ApplicationDbContext context)
        {
            _context = context;
        } 

        // GET: Procurement/Lots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lot.Include(l => l.Activity).Include(l => l.Contractor);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> AssignLotsInBulk(int id)
        {
            //var result = _context.Contractor
            //    .Where(a => a.ContractorTypeID == 1)
            //       .Select(x => new
            //       {
            //           x.ContractorID,
            //           Name = x.Name + " - " + x.CompanyName.ToString()
            //       });
            //ViewBag.ContractorID = new SelectList(result, "ContractorID", "Name");
            List<Contractor> contractorList = new List<Contractor>();
            contractorList = _context.Contractor.Where(a=>a.ContractorTypeID == 1).ToList();
            contractorList.Insert(0, new Contractor { ContractorID = 0, CompanyName = "Select" });
            ViewData["ContractorID"] = new SelectList(contractorList, "ContractorID", "CompanyName");
            var applicationDbContext = _context.Lot.Include(l => l.Activity).Include(l => l.Contractor).Where(a=>a.ActivityID == id);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index2(int id)
        {
            var pplots = _context.Lot.Include(p => p.Contractor).Include(p => p.Activity).Where(a => a.ActivityID == id);
            var result = (from p in _context.LotItem.Include(a => a.Lot).Where(a => a.Lot.ActivityID == id)
                          select new { p.lotId, p.Quantity, p.EstimatedUnitRate, p.ActualUnitRate } into x
                          group x by new { x.lotId } into g
                          select new
                          {
                              LotId = g.Key.lotId,
                              EAmount = g.Select(x => x.Quantity * x.EstimatedUnitRate).Sum(),
                              AAmount = g.Select(x => x.Quantity * x.ActualUnitRate).Sum()
                          }).ToList();
            List<int> data = new List<int>();
            foreach (var obj in result)
            {
                data.Add(obj.LotId);
                data.Add(obj.EAmount);
                data.Add(obj.AAmount ?? 0);
            }
            if (result.Count > 0)
            {
                int[] arrStrings = data.ToArray();
                ViewData["Data"] = arrStrings;
                ViewData["DataCount"] = result.Count;
            }
            return PartialView(await pplots.ToListAsync());
        }
        // GET: Procurement/Lots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lot
                .Include(l => l.Activity)
                .Include(l => l.Contractor)
                .FirstOrDefaultAsync(m => m.lotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }
        // GET: Procurement/Lots/Create
        public IActionResult AssignLot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Lot pLot = new Lot();
            var result = _context.Contractor
                .Where(a => a.ContractorTypeID == 1)
                   .Select(x => new
                   {
                       x.ContractorID,
                       Name = x.Name + " - " + x.CompanyName.ToString()
                   });
            ViewBag.ContractorID = new SelectList(result, "ContractorID", "Name");
            ViewBag.ActivityID = new SelectList(_context.Activity, "ActivityID", "Name", pLot.ActivityID);            
            ViewBag.LotID = new SelectList(_context.Lot.Where(a => a.ActivityID == id).OrderBy(a => a.lotno), "lotId", "lotno");       
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignLot(int? id, [Bind("lotId,lotno,ItemTotal,lotDescription,Attachment,ActivityID,ContractorID")] Lot lot, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                Lot lotActual = _context.Lot.Find(lot.lotId);
                lotActual.ActivityID = lot.ActivityID;
                lotActual.ContractorID = lot.ContractorID;
                lotActual.Attachment = lot.Attachment;
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Procurement\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    string AName = _context.Activity.Find(lot.ActivityID).Name;
                    AName = AName.Replace("&", "n");
                    var PPName = _context.ProcurementPlan.Find(_context.Activity.Find(lot.ActivityID).ProcurementPlanID).Name;
                    lotActual.Attachment = Path.Combine("/Documents/Procurement/", PPName + "/" + "//" + AName + "//Lots//" + lot.lotno.ToString() + "//" + fileName);//Server Path                
                    string sPath = Path.Combine(rootPath + PPName + "/" + "//" + AName + "//Lots//" + lot.lotno.ToString() + "//");
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
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var result = _context.Contractor
                .Where(a => a.ContractorTypeID == 1)
                   .Select(x => new
                   {
                       ContractorID = x.ContractorID,
                       Name = x.Name + " - " + x.CompanyName.ToString()
                   });
            ViewBag.ContractorID = new SelectList(result, "ContractorID", "Name", lot.ContractorID);
            ViewBag.ActivityID = new SelectList(_context.Activity, "ActivityID", "Name", lot.ActivityID);
            return View(lot);
        }
        // GET: Procurement/Lots/Create
        public IActionResult Create(int id)
        {
            ViewBag.AID = id;
            ViewBag.AName = _context.Activity.Where(a => a.ActivityID == id).Select(a => a.Name).FirstOrDefault().ToString();                        
            var query = _context.Activity.Where(a => a.ActivityID == id);
            ViewBag.ActivityID = new SelectList(query, "ActivityID", "Name");
            int LotTotal = query.Select(a => a.LotTotal).FirstOrDefault();
            int CurrentLot = _context.Lot.Count(a => a.ActivityID == id);
            ViewBag.LotTotal = LotTotal;
            ViewBag.CurrentLot = CurrentLot;
            ViewBag.NextLot = CurrentLot + 1;
            ViewBag.RemainingLot = LotTotal - CurrentLot;
            return View();
        }

        // POST: Procurement/Lots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("lotId,lotno,ItemTotal,lotDescription,Attachment,ActivityID,ContractorID")] Lot lot)
        {
            if (ModelState.IsValid)
            {
                lot.ContractorID = 62;
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new {lot.ActivityID });
            }
            var query = _context.Activity.Where(a => a.ActivityID == lot.ActivityID);
            ViewBag.ActivityID = new SelectList(query, "ActivityID", "Name");
            int LotTotal = query.Select(a => a.LotTotal).FirstOrDefault();
            int CurrentLot = _context.Lot.Count(a => a.ActivityID == lot.ActivityID);
            ViewBag.LotTotal = LotTotal;
            ViewBag.CurrentLot = CurrentLot;
            ViewBag.NextLot = CurrentLot + 1;
            ViewBag.RemainingLot = LotTotal - CurrentLot;
            return View(lot);
        }

        // GET: Procurement/Lots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lot.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            var result = _context.Contractor
                .Where(a => a.ContractorTypeID == 1)
                   .Select(x => new
                   {
                       x.ContractorID,
                       Name = x.Name + " - " + x.CompanyName.ToString()
                   });
            ViewBag.ContractorID = new SelectList(result, "ContractorID", "Name");
            ViewBag.ActivityID = new SelectList(_context.Activity.Where(a => a.ActivityID == lot.ActivityID), "ActivityID", "Name", lot.ActivityID);
            return View(lot);
        }

        // POST: Procurement/Lots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("lotId,lotno,ItemTotal,lotDescription,Attachment,ActivityID,ContractorID")] Lot lot)
        {
            if (id != lot.lotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.lotId))
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
            ViewData["ActivityID"] = new SelectList(_context.Activity, "ActivityID", "Description", lot.ActivityID);
            ViewData["ContractorID"] = new SelectList(_context.Set<Contractor>(), "ContractorID", "Address", lot.ContractorID);
            return View(lot);
        }

        // GET: Procurement/Lots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lot
                .Include(l => l.Activity)
                .Include(l => l.Contractor)
                .FirstOrDefaultAsync(m => m.lotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // POST: Procurement/Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lot = await _context.Lot.FindAsync(id);
            _context.Lot.Remove(lot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(int id)
        {
            return _context.Lot.Any(e => e.lotId == id);
        }
    }
}
