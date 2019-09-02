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
    public class ContractorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contractors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contractor.Include(c => c.ContractorType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contractors/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractor
                .Include(c => c.ContractorType)
                .FirstOrDefaultAsync(m => m.ContractorID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }
        // GET: Contractors/Create
        public IActionResult Create2(short id)
        {            
            ViewData["ContractorTypeID"] = new SelectList(_context.Set<ContractorType>().Where(a => a.ContractorTypeID == id), "ContractorTypeID", "ContractorTypeID");
            return PartialView();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(short id, [Bind("ContractorID,Name,Contact,Email,CompanyName,NTNNo,Address,ContractorTypeID")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                contractor.ContractorTypeID = id;
                _context.Add(contractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), "ActivityDetails",new { area = "Procurement", ActivityID = 7 , StepID = 8 });
            }
            ViewData["ContractorTypeID"] = new SelectList(_context.Set<ContractorType>(), "ContractorTypeID", "ContractorTypeID", contractor.ContractorTypeID);
            return View(contractor);
        }
        // GET: Contractors/Create
        public IActionResult Create()
        {
            ViewData["ContractorTypeID"] = new SelectList(_context.Set<ContractorType>(), "ContractorTypeID", "ContractorTypeID");
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractorID,Name,Contact,Email,CompanyName,NTNNo,Address,ContractorTypeID")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractorTypeID"] = new SelectList(_context.Set<ContractorType>(), "ContractorTypeID", "ContractorTypeID", contractor.ContractorTypeID);
            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractor.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            ViewData["ContractorTypeID"] = new SelectList(_context.Set<ContractorType>(), "ContractorTypeID", "ContractorTypeID", contractor.ContractorTypeID);
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ContractorID,Name,Contact,Email,CompanyName,NTNNo,Address,ContractorTypeID")] Contractor contractor)
        {
            if (id != contractor.ContractorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractor.ContractorID))
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
            ViewData["ContractorTypeID"] = new SelectList(_context.Set<ContractorType>(), "ContractorTypeID", "ContractorTypeID", contractor.ContractorTypeID);
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractor
                .Include(c => c.ContractorType)
                .FirstOrDefaultAsync(m => m.ContractorID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var contractor = await _context.Contractor.FindAsync(id);
            _context.Contractor.Remove(contractor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractorExists(short id)
        {
            return _context.Contractor.Any(e => e.ContractorID == id);
        }
    }
}
