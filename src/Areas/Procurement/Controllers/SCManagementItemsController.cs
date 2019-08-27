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
    public class SCManagementItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SCManagementItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/SCManagementItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SCManagementItem.Include(s => s.LotItem).Include(s => s.SCManagement);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Procurement/SCManagementItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCManagementItem = await _context.SCManagementItem
                .Include(s => s.LotItem)
                .Include(s => s.SCManagement)
                .FirstOrDefaultAsync(m => m.SCManagementItemID == id);
            if (sCManagementItem == null)
            {
                return NotFound();
            }

            return View(sCManagementItem);
        }

        // GET: Procurement/SCManagementItems/Create
        public IActionResult Create()
        {
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId");
            ViewData["SCManagementID"] = new SelectList(_context.SCManagement, "SCManagementID", "SCManagementID");
            return View();
        }

        // POST: Procurement/SCManagementItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SCManagementItemID,SCManagementID,LotItemId,FQuantity,ActualUnitRate,Quantity")] SCManagementItem sCManagementItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sCManagementItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", sCManagementItem.LotItemId);
            ViewData["SCManagementID"] = new SelectList(_context.SCManagement, "SCManagementID", "SCManagementID", sCManagementItem.SCManagementID);
            return View(sCManagementItem);
        }

        // GET: Procurement/SCManagementItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCManagementItem = await _context.SCManagementItem.FindAsync(id);
            if (sCManagementItem == null)
            {
                return NotFound();
            }
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", sCManagementItem.LotItemId);
            ViewData["SCManagementID"] = new SelectList(_context.SCManagement, "SCManagementID", "SCManagementID", sCManagementItem.SCManagementID);
            return View(sCManagementItem);
        }

        // POST: Procurement/SCManagementItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SCManagementItemID,SCManagementID,LotItemId,FQuantity,ActualUnitRate,Quantity")] SCManagementItem sCManagementItem)
        {
            if (id != sCManagementItem.SCManagementItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sCManagementItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SCManagementItemExists(sCManagementItem.SCManagementItemID))
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
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", sCManagementItem.LotItemId);
            ViewData["SCManagementID"] = new SelectList(_context.SCManagement, "SCManagementID", "SCManagementID", sCManagementItem.SCManagementID);
            return View(sCManagementItem);
        }

        // GET: Procurement/SCManagementItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sCManagementItem = await _context.SCManagementItem
                .Include(s => s.LotItem)
                .Include(s => s.SCManagement)
                .FirstOrDefaultAsync(m => m.SCManagementItemID == id);
            if (sCManagementItem == null)
            {
                return NotFound();
            }

            return View(sCManagementItem);
        }

        // POST: Procurement/SCManagementItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sCManagementItem = await _context.SCManagementItem.FindAsync(id);
            _context.SCManagementItem.Remove(sCManagementItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SCManagementItemExists(int id)
        {
            return _context.SCManagementItem.Any(e => e.SCManagementItemID == id);
        }
    }
}
