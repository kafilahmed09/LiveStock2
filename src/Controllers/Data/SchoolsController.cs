using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Models.Data;
using BES.Data;

namespace BES.Controllers.Data
{
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schools
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schools.Include(s => s.UC);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.UC)
                .FirstOrDefaultAsync(m => m.SchoolID == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // GET: Schools/Create
        public IActionResult Create()
        {
            ViewData["UCID"] = new SelectList(_context.UCs, "UCID", "UCName");
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolID,UCID,SName,SchoolOf,SCode,BEMIS,Level,Latitude,Longitude,Status,Zone,Onboard,DateOpen,Abandon,Upgradation,ConstructionStartDate,Budget,type,PTSMC,LandMutation,InitialEnrollment,Construction,Password,SelectedStatus,Remarks,NotificationDate,Phase")] School school)
        {
            if (ModelState.IsValid)
            {
                _context.Add(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UCID"] = new SelectList(_context.UCs, "UCID", "UCName", school.UCID);
            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            ViewData["UCID"] = new SelectList(_context.UCs, "UCID", "UCName", school.UCID);
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchoolID,UCID,SName,SchoolOf,SCode,BEMIS,Level,Latitude,Longitude,Status,Zone,Onboard,DateOpen,Abandon,Upgradation,ConstructionStartDate,Budget,type,PTSMC,LandMutation,InitialEnrollment,Construction,Password,SelectedStatus,Remarks,NotificationDate,Phase")] School school)
        {
            if (id != school.SchoolID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(school);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.SchoolID))
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
            ViewData["UCID"] = new SelectList(_context.UCs, "UCID", "UCName", school.UCID);
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.UC)
                .FirstOrDefaultAsync(m => m.SchoolID == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolID == id);
        }
    }
}
