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
    public class SCManagementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SCManagementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/SCManagements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SCManagement.Include(s => s.Location).Include(s => s.Lot);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Procurement/SCManagements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCManagement = await _context.SCManagement
                .Include(s => s.Location)
                .Include(s => s.Lot)
                .FirstOrDefaultAsync(m => m.SCManagementID == id);
            if (sCManagement == null)
            {
                return NotFound();
            }

            return View(sCManagement);
        }

        // GET: Procurement/SCManagements/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId");
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId");
            return View();
        }

        // POST: Procurement/SCManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SCManagementID,LotId,ReceivingDate,LocationId,Attachment")] SCManagement sCManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sCManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", sCManagement.LocationId);
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId", sCManagement.LotId);
            return View(sCManagement);
        }

        // GET: Procurement/SCManagements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCManagement = await _context.SCManagement.FindAsync(id);
            if (sCManagement == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", sCManagement.LocationId);
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId", sCManagement.LotId);
            return View(sCManagement);
        }

        // POST: Procurement/SCManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SCManagementID,LotId,ReceivingDate,LocationId,Attachment")] SCManagement sCManagement)
        {
            if (id != sCManagement.SCManagementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sCManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SCManagementExists(sCManagement.SCManagementID))
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
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", sCManagement.LocationId);
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId", sCManagement.LotId);
            return View(sCManagement);
        }

        // GET: Procurement/SCManagements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCManagement = await _context.SCManagement
                .Include(s => s.Location)
                .Include(s => s.Lot)
                .FirstOrDefaultAsync(m => m.SCManagementID == id);
            if (sCManagement == null)
            {
                return NotFound();
            }

            return View(sCManagement);
        }

        // POST: Procurement/SCManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sCManagement = await _context.SCManagement.FindAsync(id);
            _context.SCManagement.Remove(sCManagement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SCManagementExists(int id)
        {
            return _context.SCManagement.Any(e => e.SCManagementID == id);
        }
    }
}
