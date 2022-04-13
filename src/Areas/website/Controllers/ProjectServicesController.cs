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
    public class ProjectServicesController : BaselineController
    {        
        public ProjectServicesController(ApplicationDbContext context) : base(context)
        {            
        }

        // GET: website/ProjectServices
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;            
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            var applicationDbContext = _context.ProjectServices.Include(p => p.ServiceType);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Weather()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;            
            return View();
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.ProjectServices.Include(p=>p.ServiceType).ToListAsync());
        }

        // GET: website/ProjectServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectService = await _context.ProjectServices
                .Include(p => p.ServiceType)
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (projectService == null)
            {
                return NotFound();
            }

            return View(projectService);
        }

        // GET: website/ProjectServices/Create
        [Authorize(Roles = "Website")]
        public IActionResult Create()
        {
            ProjectService Obj = new ProjectService();
            Obj.Visibility = true;
            Obj.CreatedOn = DateTime.Now.Date;            
            ViewData["ServiceTypeID"] = new SelectList(_context.ServiceType, "ServiceTypeID", "Name");
            return View(Obj);
        }

        // POST: website/ProjectServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,ServiceTypeID,SerialNo,Name,Visibility,CreatedOn")] ProjectService projectService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["ServiceTypeID"] = new SelectList(_context.ServiceType, "ServiceTypeID", "Name", projectService.ServiceTypeID);
            return View(projectService);
        }

        // GET: website/ProjectServices/Edit/5
        [Authorize(Roles = "Website")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectService = await _context.ProjectServices.FindAsync(id);
            if (projectService == null)
            {
                return NotFound();
            }
            ViewData["ServiceTypeID"] = new SelectList(_context.ServiceType, "ServiceTypeID", "Name", projectService.ServiceTypeID);
            return View(projectService);
        }

        // POST: website/ProjectServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceID,ServiceTypeID,SerialNo,Name,Visibility,CreatedOn")] ProjectService projectService)
        {
            if (id != projectService.ServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectServiceExists(projectService.ServiceID))
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
            ViewData["ServiceTypeID"] = new SelectList(_context.ServiceType, "ServiceTypeID", "Name", projectService.ServiceTypeID);
            return View(projectService);
        }

        // GET: website/ProjectServices/Delete/5
        [Authorize(Roles = "Website")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectService = await _context.ProjectServices
                .Include(p => p.ServiceType)
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (projectService == null)
            {
                return NotFound();
            }

            return View(projectService);
        }

        // POST: website/ProjectServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectService = await _context.ProjectServices.FindAsync(id);
            _context.ProjectServices.Remove(projectService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectServiceExists(int id)
        {
            return _context.ProjectServices.Any(e => e.ServiceID == id);
        }
    }
}
