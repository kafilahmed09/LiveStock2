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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class InitiativesController : BaselineController
    {
        public InitiativesController(ApplicationDbContext context) : base(context)
        {            
        }

        // GET: website/Initiatives
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.Initiative.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Initiative.OrderByDescending(a => a.InitiativeId).ToListAsync());
        }
        // GET: website/Initiatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var initiative = await _context.Initiative
                .FirstOrDefaultAsync(m => m.InitiativeId == id);
            if (initiative == null)
            {
                return NotFound();
            }

            return View(initiative);
        }

        // GET: website/Initiatives/Create
        [Authorize(Roles = "Website")]
        public IActionResult Create()
        {
            Initiative Obj = new Initiative();
            Obj.CreatedOn = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/Initiatives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InitiativeId,Name,filepath,CreatedOn")] Initiative initiative, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Initiatives\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Notif" + randomNumber.ToString() + fileName;
                    initiative.filepath = Path.Combine("/Documents/Initiatives/", fileName);//Server Path
                    string sPath = Path.Combine(rootPath);
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    string FullPathWithFileName = Path.Combine(sPath, fileName);
                    using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                    {
                        await Attachment.CopyToAsync(stream);
                    }
                }
                _context.Add(initiative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(initiative);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Initiatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var initiative = await _context.Initiative.FindAsync(id);
            if (initiative == null)
            {
                return NotFound();
            }
            return View(initiative);
        }

        // POST: website/Initiatives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InitiativeId,Name,filepath,CreatedOn")] Initiative initiative, IFormFile Attachment)
        {
            if (id != initiative.InitiativeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Attachment != null)
                    {
                        var rootPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Initiatives\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Notif" + randomNumber.ToString() + fileName;
                        initiative.filepath = Path.Combine("/Documents/Initiatives/", fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await Attachment.CopyToAsync(stream);
                        }
                    }
                    _context.Update(initiative);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InitiativeExists(initiative.InitiativeId))
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
            return View(initiative);
        }

        [Authorize(Roles = "Website")]
        // GET: website/Initiatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var initiative = await _context.Initiative
                .FirstOrDefaultAsync(m => m.InitiativeId == id);
            if (initiative == null)
            {
                return NotFound();
            }

            return View(initiative);
        }

        // POST: website/Initiatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var initiative = await _context.Initiative.FindAsync(id);
            _context.Initiative.Remove(initiative);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InitiativeExists(int id)
        {
            return _context.Initiative.Any(e => e.InitiativeId == id);
        }
    }
}
