using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class PublicationsController : BaselineController
    {
        public PublicationsController(ApplicationDbContext context) : base(context)
        {
        }
        // GET: website/publications
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.Publication.Where(a => a.Visibility == true).ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Publication.ToListAsync());
        }
        // GET: website/publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.PublicationID == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }
        [Authorize(Roles = "Website")]
        // GET: website/publications/Create
        public IActionResult Create()
        {
            Publication Obj = new Publication();
            Obj.Visibility = true;
            Obj.CreatedOn = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicationID,Name,FilePath,Visibility,AuthorName,CreatedOn")] Publication publication, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\publication\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Publication" + randomNumber.ToString() + fileName;
                    publication.FilePath = Path.Combine("/Documents/publication/", fileName);//Server Path
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
                publication.Visibility = true;
                _context.Add(publication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(publication);
        }
        [Authorize(Roles = "Website")]
        // GET: website/publications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            return View(publication);
        }

        // POST: website/publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicationID,Name,FilePath,AuthorName,Visibility,CreatedOn,ShowOnSlider")] Publication publication, IFormFile Attachment)
        {
            if (id != publication.PublicationID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\publication\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Publication" + randomNumber.ToString() + fileName;
                        publication.FilePath = Path.Combine("/Documents/publication/", fileName);//Server Path
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
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!publicationExists(publication.PublicationID))
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
            return View(publication);
        }
        [Authorize(Roles = "Website")]
        // GET: website/publications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.PublicationID == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: website/publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publication = await _context.Publication.FindAsync(id);
            _context.Publication.Remove(publication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool publicationExists(int id)
        {
            return _context.Publication.Any(e => e.PublicationID == id);
        }
    }
}
