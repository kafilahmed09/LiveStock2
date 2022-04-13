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
    public class ServiceCentersController : BaselineController
    {        
        public ServiceCentersController(ApplicationDbContext context) : base(context)
        {            
        }

        // GET: website/ServiceCenters
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;            
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.ServiceCenter.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.ServiceCenter.ToListAsync());
        }

        // GET: website/ServiceCenters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCenter = await _context.ServiceCenter
                .FirstOrDefaultAsync(m => m.ServiceCenterID == id);
            if (serviceCenter == null)
            {
                return NotFound();
            }

            return View(serviceCenter);
        }

        // GET: website/ServiceCenters/Create
        [Authorize(Roles = "Website")]
        public IActionResult Create()
        {
            ServiceCenter Obj = new ServiceCenter();
            Obj.Visibility = true;
            Obj.CreatedOn = DateTime.Now.Date;            
            return View(Obj);
        }

        // POST: website/ServiceCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceCenterID,SerialNo,Name,Description,Numbers,Visibility,CreatedOn")] ServiceCenter serviceCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(serviceCenter);
        }

        // GET: website/ServiceCenters/Edit/5
        [Authorize(Roles = "Website")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCenter = await _context.ServiceCenter.FindAsync(id);
            if (serviceCenter == null)
            {
                return NotFound();
            }
            return View(serviceCenter);
        }

        // POST: website/ServiceCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceCenterID,SerialNo,Name,Description,Numbers,Visibility,CreatedOn")] ServiceCenter serviceCenter)
        {
            if (id != serviceCenter.ServiceCenterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceCenterExists(serviceCenter.ServiceCenterID))
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
            return View(serviceCenter);
        }

        // GET: website/ServiceCenters/Delete/5
        [Authorize(Roles = "Website")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCenter = await _context.ServiceCenter
                .FirstOrDefaultAsync(m => m.ServiceCenterID == id);
            if (serviceCenter == null)
            {
                return NotFound();
            }

            return View(serviceCenter);
        }

        // POST: website/ServiceCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCenter = await _context.ServiceCenter.FindAsync(id);
            _context.ServiceCenter.Remove(serviceCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceCenterExists(int id)
        {
            return _context.ServiceCenter.Any(e => e.ServiceCenterID == id);
        }
    }
}
