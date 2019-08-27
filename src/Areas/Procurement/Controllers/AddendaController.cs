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
    public class AddendaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddendaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/Addenda
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Addendum.Include(a => a.AddendumType).Include(a => a.Lot);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Procurement/Addenda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendum = await _context.Addendum
                .Include(a => a.AddendumType)
                .Include(a => a.Lot)
                .FirstOrDefaultAsync(m => m.AddendumId == id);
            if (addendum == null)
            {
                return NotFound();
            }

            return View(addendum);
        }

        // GET: Procurement/Addenda/Create
        public IActionResult Create()
        {
            ViewData["AddendumTypeId"] = new SelectList(_context.Set<AddendumType>(), "AddendumTypeId", "AddendumTypeId");
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId");
            return View();
        }

        // POST: Procurement/Addenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddendumId,LotId,AddendumTypeId,Attachment,Remarks,ExpiryDate")] Addendum addendum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addendum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddendumTypeId"] = new SelectList(_context.Set<AddendumType>(), "AddendumTypeId", "AddendumTypeId", addendum.AddendumTypeId);
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId", addendum.LotId);
            return View(addendum);
        }

        // GET: Procurement/Addenda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendum = await _context.Addendum.FindAsync(id);
            if (addendum == null)
            {
                return NotFound();
            }
            ViewData["AddendumTypeId"] = new SelectList(_context.Set<AddendumType>(), "AddendumTypeId", "AddendumTypeId", addendum.AddendumTypeId);
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId", addendum.LotId);
            return View(addendum);
        }

        // POST: Procurement/Addenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddendumId,LotId,AddendumTypeId,Attachment,Remarks,ExpiryDate")] Addendum addendum)
        {
            if (id != addendum.AddendumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addendum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddendumExists(addendum.AddendumId))
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
            ViewData["AddendumTypeId"] = new SelectList(_context.Set<AddendumType>(), "AddendumTypeId", "AddendumTypeId", addendum.AddendumTypeId);
            ViewData["LotId"] = new SelectList(_context.Lot, "lotId", "lotId", addendum.LotId);
            return View(addendum);
        }

        // GET: Procurement/Addenda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendum = await _context.Addendum
                .Include(a => a.AddendumType)
                .Include(a => a.Lot)
                .FirstOrDefaultAsync(m => m.AddendumId == id);
            if (addendum == null)
            {
                return NotFound();
            }

            return View(addendum);
        }

        // POST: Procurement/Addenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addendum = await _context.Addendum.FindAsync(id);
            _context.Addendum.Remove(addendum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddendumExists(int id)
        {
            return _context.Addendum.Any(e => e.AddendumId == id);
        }
    }
}
