using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class ImportantLinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportantLinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/ImportantLinks
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportantLink.ToListAsync());
        }
        // GET: website/ImportantLinks
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.ImportantLink.ToListAsync());
        }
        // GET: website/ImportantLinks/Details/5      
        [Authorize(Roles = "Website")]
        // GET: website/ImportantLinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: website/ImportantLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportantLinkID,Name,Address")] ImportantLink importantLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importantLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(importantLink);
        }
        [Authorize(Roles = "Website")]
        // GET: website/ImportantLinks/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importantLink = await _context.ImportantLink.FindAsync(id);
            if (importantLink == null)
            {
                return NotFound();
            }
            return View(importantLink);
        }

        // POST: website/ImportantLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ImportantLinkID,Name,Address")] ImportantLink importantLink)
        {
            if (id != importantLink.ImportantLinkID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importantLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportantLinkExists(importantLink.ImportantLinkID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create));
            }
            return View(importantLink);
        }
        [Authorize(Roles = "Website")]
        // GET: website/ImportantLinks/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importantLink = await _context.ImportantLink
                .FirstOrDefaultAsync(m => m.ImportantLinkID == id);
            if (importantLink == null)
            {
                return NotFound();
            }

            return View(importantLink);
        }

        // POST: website/ImportantLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var importantLink = await _context.ImportantLink.FindAsync(id);
            _context.ImportantLink.Remove(importantLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool ImportantLinkExists(short id)
        {
            return _context.ImportantLink.Any(e => e.ImportantLinkID == id);
        }
    }
}
