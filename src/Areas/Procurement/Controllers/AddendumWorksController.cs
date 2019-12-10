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
    public class AddendumWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddendumWorksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/AddendumWorks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AddendumWorks.Include(a => a.ActivityDetailWork).Include(a => a.AddendumType);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> PartialIndex(short id)
        {
            var applicationDbContext = _context.AddendumWorks.Include(a => a.ActivityDetailWork.Activity).Include(a => a.AddendumType).Where(a => a.ActivityDetailWork.ActivityID == id);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        // GET: Procurement/AddendumWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumWorks = await _context.AddendumWorks
                .Include(a => a.ActivityDetailWork)
                .Include(a => a.AddendumType)
                .FirstOrDefaultAsync(m => m.AddendumId == id);
            if (addendumWorks == null)
            {
                return NotFound();
            }

            return View(addendumWorks);
        }

        // GET: Procurement/AddendumWorks/Create
        public IActionResult Create(short id)
        {
            ViewBag.AName = _context.Activity.Find(id).Name;
            short val = _context.ActivityDetail.Where(a => a.ActivityID == id && a.StepID == 21).Select(a => a.StepID).FirstOrDefault();
            if (val < 1)
            {
                ViewBag.Pending = "Before Award of contract, you are unable to add Addendum!";
            }
            ViewBag.AID = id;
            ViewData["AddendumTypeId"] = new SelectList(_context.AddendumType, "AddendumTypeId", "Name");
            return View();
        }

        // POST: Procurement/AddendumWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddendumId,ActivityDetailWorkID,AddendumTypeId,Attachment,Remarks,ExpiryDate,CurrentDate,ActualAmount")] AddendumWorks addendumWorks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addendumWorks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityDetailWorkID"] = new SelectList(_context.ActivityDetailWork, "ActivityDetailWorkID", "ActivityDetailWorkID", addendumWorks.ActivityDetailWorkID);
            ViewData["AddendumTypeId"] = new SelectList(_context.AddendumType, "AddendumTypeId", "AddendumTypeId", addendumWorks.AddendumTypeId);
            return View(addendumWorks);
        }

        // GET: Procurement/AddendumWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumWorks = await _context.AddendumWorks.FindAsync(id);
            if (addendumWorks == null)
            {
                return NotFound();
            }
            ViewData["ActivityDetailWorkID"] = new SelectList(_context.ActivityDetailWork, "ActivityDetailWorkID", "ActivityDetailWorkID", addendumWorks.ActivityDetailWorkID);
            ViewData["AddendumTypeId"] = new SelectList(_context.AddendumType, "AddendumTypeId", "AddendumTypeId", addendumWorks.AddendumTypeId);
            return View(addendumWorks);
        }

        // POST: Procurement/AddendumWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddendumId,ActivityDetailWorkID,AddendumTypeId,Attachment,Remarks,ExpiryDate,CurrentDate,ActualAmount")] AddendumWorks addendumWorks)
        {
            if (id != addendumWorks.AddendumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addendumWorks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddendumWorksExists(addendumWorks.AddendumId))
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
            ViewData["ActivityDetailWorkID"] = new SelectList(_context.ActivityDetailWork, "ActivityDetailWorkID", "ActivityDetailWorkID", addendumWorks.ActivityDetailWorkID);
            ViewData["AddendumTypeId"] = new SelectList(_context.AddendumType, "AddendumTypeId", "AddendumTypeId", addendumWorks.AddendumTypeId);
            return View(addendumWorks);
        }

        // GET: Procurement/AddendumWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumWorks = await _context.AddendumWorks
                .Include(a => a.ActivityDetailWork)
                .Include(a => a.AddendumType)
                .FirstOrDefaultAsync(m => m.AddendumId == id);
            if (addendumWorks == null)
            {
                return NotFound();
            }

            return View(addendumWorks);
        }

        // POST: Procurement/AddendumWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addendumWorks = await _context.AddendumWorks.FindAsync(id);
            _context.AddendumWorks.Remove(addendumWorks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddendumWorksExists(int id)
        {
            return _context.AddendumWorks.Any(e => e.AddendumId == id);
        }
    }
}
