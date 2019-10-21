using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Data;
using BES.Models.Data;

namespace BES.Controllers.Data
{
    public class IndicatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IndicatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Indicators
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Indicator.Include(i => i.Partner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Indicators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicator
                .Include(i => i.Partner)
                .FirstOrDefaultAsync(m => m.IndicatorID == id);
            if (indicator == null)
            {
                return NotFound();
            }

            return View(indicator);
        }

        // GET: Indicators/Create
        public IActionResult Create()
        {
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerName");
            ViewData["InputType"] = new SelectList(new[]
                    {
                        new { Id = "N/A", Name = "N/A" },
                        new { Id = "image/*", Name = "image" },
                        new { Id = "application/pdf", Name = "PDF" }
                    }, "Id", "Name");
            return View();
        }

        // POST: Indicators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IndicatorID,PartnerID,IndicatorName,Description,SequenceNo,IsEvidenceRequire,IsPotential,IsFeeder,IsNextLevel,EvidanceType")] Indicator indicator)
        {
            if (ModelState.IsValid)
            {
                indicator.IndicatorID = _context.Indicator.Max(a => a.IndicatorID)+1;
                _context.Add(indicator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerName", indicator.PartnerID);
            ViewData["InputType"] = new SelectList(new[]
                 {
                        new { Id = "N/A", Name = "N/A" },
                        new { Id = "image/*", Name = "image" },
                        new { Id = "application/pdf", Name = "PDF" }
                    }, "Id", "Name");
            return View(indicator);
        }

        // GET: Indicators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicator.FindAsync(id);
            if (indicator == null)
            {
                return NotFound();
            }
            ViewData["InputType"] = new SelectList(new[]
                {
                        new { Id = "N/A", Name = "N/A" },
                        new { Id = "image/*", Name = "image" },
                        new { Id = "application/pdf", Name = "PDF" }
                    }, "Id", "Name");
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerName", indicator.PartnerID);
            return View(indicator);
        }

        // POST: Indicators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IndicatorID,PartnerID,IndicatorName,Description,SequenceNo,IsEvidenceRequire,IsPotential,IsFeeder,IsNextLevel,EvidanceType")] Indicator indicator)
        {
            if (id != indicator.IndicatorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indicator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndicatorExists(indicator.IndicatorID))
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
            ViewData["PartnerID"] = new SelectList(_context.Set<Partner>(), "PartnerID", "PartnerName", indicator.PartnerID);

            ViewData["InputType"] = new SelectList(new[]
                {
                        new { Id = "N/A", Name = "N/A" },
                        new { Id = "image/*", Name = "image" },
                        new { Id = "application/pdf", Name = "PDF" }
                    }, "Id", "Name"); return View(indicator);
        }

        // GET: Indicators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indicator = await _context.Indicator
                .Include(i => i.Partner)
                .FirstOrDefaultAsync(m => m.IndicatorID == id);
            if (indicator == null)
            {
                return NotFound();
            }

            return View(indicator);
        }

        // POST: Indicators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var indicator = await _context.Indicator.FindAsync(id);
            _context.Indicator.Remove(indicator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndicatorExists(int id)
        {
            return _context.Indicator.Any(e => e.IndicatorID == id);
        }
    }
}
