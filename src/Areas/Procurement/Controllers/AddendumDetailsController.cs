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
    public class AddendumDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddendumDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/AddendumDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AddendumDetail.Include(a => a.Addendum).Include(a => a.LotItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Procurement/AddendumDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumDetail = await _context.AddendumDetail
                .Include(a => a.Addendum)
                .Include(a => a.LotItem)
                .FirstOrDefaultAsync(m => m.AddendumDetailId == id);
            if (addendumDetail == null)
            {
                return NotFound();
            }

            return View(addendumDetail);
        }

        // GET: Procurement/AddendumDetails/Create
        public IActionResult Create()
        {
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId");
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId");
            return View();
        }

        // POST: Procurement/AddendumDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddendumDetailId,AddendumId,LotItemId,Quantity")] AddendumDetail addendumDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addendumDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId", addendumDetail.AddendumId);
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", addendumDetail.LotItemId);
            return View(addendumDetail);
        }

        // GET: Procurement/AddendumDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumDetail = await _context.AddendumDetail.FindAsync(id);
            if (addendumDetail == null)
            {
                return NotFound();
            }
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId", addendumDetail.AddendumId);
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", addendumDetail.LotItemId);
            return View(addendumDetail);
        }

        // POST: Procurement/AddendumDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddendumDetailId,AddendumId,LotItemId,Quantity")] AddendumDetail addendumDetail)
        {
            if (id != addendumDetail.AddendumDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addendumDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddendumDetailExists(addendumDetail.AddendumDetailId))
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
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId", addendumDetail.AddendumId);
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", addendumDetail.LotItemId);
            return View(addendumDetail);
        }

        // GET: Procurement/AddendumDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumDetail = await _context.AddendumDetail
                .Include(a => a.Addendum)
                .Include(a => a.LotItem)
                .FirstOrDefaultAsync(m => m.AddendumDetailId == id);
            if (addendumDetail == null)
            {
                return NotFound();
            }

            return View(addendumDetail);
        }

        // POST: Procurement/AddendumDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addendumDetail = await _context.AddendumDetail.FindAsync(id);
            _context.AddendumDetail.Remove(addendumDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddendumDetailExists(int id)
        {
            return _context.AddendumDetail.Any(e => e.AddendumDetailId == id);
        }
    }
}
