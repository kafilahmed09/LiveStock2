using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class TendersController : BaselineController
    {        
        public TendersController(ApplicationDbContext context) : base(context)
        {            
        }
        // GET: website/Tenders
        public async Task<IActionResult> Index(bool id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.Tender.Where(a=>a.Status == id && a.Visibility == true).ToListAsync());
        }
        // GET: website/Tenders
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Tender.ToListAsync());
        }
        // GET: website/Tenders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tender = await _context.Tender
                .FirstOrDefaultAsync(m => m.TenderID == id);
            if (tender == null)
            {
                return NotFound();
            }

            return View(tender);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Tenders/Create
        public IActionResult Create()
        {
            Tender Obj = new Tender();
            Obj.Status = true;
            Obj.Visibility = true;
            Obj.OpenDate = DateTime.Now.Date;
            Obj.CloseDate = DateTime.Now.Date.AddDays(10);
            return View(Obj);
        }

        // POST: website/Tenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenderID,Name,FilePath,Visibility,Status,OpenDate,CloseDate,CreatedOn")] Tender tender, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Tender\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Tender" + randomNumber.ToString() + fileName;
                    tender.FilePath = Path.Combine("/Documents/Tender/", fileName);//Server Path
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
                    tender.CreatedOn = DateTime.Now.Date;
                    _context.Add(tender);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }
                
            }
            return View(tender);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Tenders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tender = await _context.Tender.FindAsync(id);
            if (tender == null)
            {
                return NotFound();
            }
            return View(tender);
        }

        // POST: website/Tenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenderID,Name,FilePath,Visibility,Status,OpenDate,CloseDate,CreatedOn")] Tender tender, IFormFile Attachment)
        {
            if (id != tender.TenderID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Tender\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Tender" + randomNumber.ToString() + fileName;
                        tender.FilePath = Path.Combine("/Documents/Tender/", fileName);//Server Path
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
                    _context.Update(tender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenderExists(tender.TenderID))
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
            return View(tender);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Tenders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tender = await _context.Tender
                .FirstOrDefaultAsync(m => m.TenderID == id);
            if (tender == null)
            {
                return NotFound();
            }

            return View(tender);
        }

        // POST: website/Tenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tender = await _context.Tender.FindAsync(id);
            _context.Tender.Remove(tender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool TenderExists(int id)
        {
            return _context.Tender.Any(e => e.TenderID == id);
        }
    }
}
