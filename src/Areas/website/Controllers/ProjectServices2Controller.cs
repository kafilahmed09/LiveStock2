using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class ProjectServices2Controller : BaselineController
    {        

        public ProjectServices2Controller(ApplicationDbContext context) : base(context)
        {            
        }

        // GET: website/Services
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.ProjectServices.Where(a => a.Visibility == true).ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.ProjectServices.ToListAsync());
        }
        // GET: website/Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = await _context.ProjectServices
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // GET: website/Services/Create
        public IActionResult Create()
        {
            ProjectService Obj = new ProjectService();
            Obj.Visibility = true;
            Obj.CreatedOn = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,SerialNo,Name,Visibility,CreatedOn")] ProjectService services)
        {
            if (ModelState.IsValid)
            {
                _context.Add(services);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(services);
        }

        // GET: website/Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = await _context.ProjectServices.FindAsync(id);
            if (services == null)
            {
                return NotFound();
            }
            return View(services);
        }

        // POST: website/Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceID,SerialNo,Name,Visibility,CreatedOn")] ProjectService services)
        {
            if (id != services.ServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(services);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(services.ServiceID))
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
            return View(services);
        }

        // GET: website/Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = await _context.ProjectServices
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // POST: website/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var services = await _context.ProjectServices.FindAsync(id);
            _context.ProjectServices.Remove(services);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesExists(int id)
        {
            return _context.ProjectServices.Any(e => e.ServiceID == id);
        }
    }
}
