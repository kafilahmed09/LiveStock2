using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Areas.Procurement.Models;
using BES.Data;

namespace BES.Areas.Procurement.Controllers
{
    [Area("Procurement")]
    public class StepsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/Steps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Step.Include(s => s.ProcurementPlan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Procurement/Steps/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step
                .Include(s => s.ProcurementPlan)
                .FirstOrDefaultAsync(m => m.StepID == id);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }

        // GET: Procurement/Steps/Create
        public IActionResult Create()
        {
            ViewData["ProcurementPlanID"] = new SelectList(_context.ProcurementPlan, "ProcurementPlanID", "ProcurementPlanID");
            return View();
        }

        // POST: Procurement/Steps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StepID,ProcurementPlanID,Name,SerailNo")] Step step)
        {
            if (ModelState.IsValid)
            {
                _context.Add(step);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcurementPlanID"] = new SelectList(_context.ProcurementPlan, "ProcurementPlanID", "ProcurementPlanID", step.ProcurementPlanID);
            return View(step);
        }

        // GET: Procurement/Steps/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }
            ViewData["ProcurementPlanID"] = new SelectList(_context.ProcurementPlan, "ProcurementPlanID", "ProcurementPlanID", step.ProcurementPlanID);
            return View(step);
        }

        // POST: Procurement/Steps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("StepID,ProcurementPlanID,Name,SerailNo")] Step step)
        {
            if (id != step.StepID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(step);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StepExists(step.StepID))
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
            ViewData["ProcurementPlanID"] = new SelectList(_context.ProcurementPlan, "ProcurementPlanID", "ProcurementPlanID", step.ProcurementPlanID);
            return View(step);
        }

        // GET: Procurement/Steps/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Step
                .Include(s => s.ProcurementPlan)
                .FirstOrDefaultAsync(m => m.StepID == id);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }

        // POST: Procurement/Steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var step = await _context.Step.FindAsync(id);
            _context.Step.Remove(step);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StepExists(short id)
        {
            return _context.Step.Any(e => e.StepID == id);
        }
    }
}
