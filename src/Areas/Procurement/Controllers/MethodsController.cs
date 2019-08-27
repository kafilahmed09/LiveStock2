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
    public class MethodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MethodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/Methods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Method.ToListAsync());
        }

        // GET: Procurement/Methods/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @method = await _context.Method
                .FirstOrDefaultAsync(m => m.MethodID == id);
            if (@method == null)
            {
                return NotFound();
            }

            return View(@method);
        }

        // GET: Procurement/Methods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Procurement/Methods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MethodID,Name")] Method @method)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@method);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@method);
        }

        // GET: Procurement/Methods/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @method = await _context.Method.FindAsync(id);
            if (@method == null)
            {
                return NotFound();
            }
            return View(@method);
        }

        // POST: Procurement/Methods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("MethodID,Name")] Method @method)
        {
            if (id != @method.MethodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@method);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MethodExists(@method.MethodID))
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
            return View(@method);
        }

        // GET: Procurement/Methods/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @method = await _context.Method
                .FirstOrDefaultAsync(m => m.MethodID == id);
            if (@method == null)
            {
                return NotFound();
            }

            return View(@method);
        }

        // POST: Procurement/Methods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var @method = await _context.Method.FindAsync(id);
            _context.Method.Remove(@method);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MethodExists(short id)
        {
            return _context.Method.Any(e => e.MethodID == id);
        }
    }
}
