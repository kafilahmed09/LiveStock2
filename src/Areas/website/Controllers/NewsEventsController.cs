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
    public class NewsEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/NewsEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsEvent.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.NewsEvent.ToListAsync());
        }
        // GET: website/NewsEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsEvent = await _context.NewsEvent
                .FirstOrDefaultAsync(m => m.NewsEventID == id);
            if (newsEvent == null)
            {
                return NotFound();
            }
            ViewBag.pictures = await _context.NewsEventPicture.Where(a => a.NewsEventID == id).ToListAsync();
            return View(newsEvent);
        }
        [Authorize(Roles = "Website")]
        // GET: website/NewsEvents/Create
        public IActionResult Create()
        {
            NewsEvent Obj = new NewsEvent();
            Obj.Visibility = true;
            Obj.OnDate = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/NewsEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsEventID,Heading,Description,OnDate,CreatedOn,Visibility")] NewsEvent newsEvent, List<IFormFile> Attachment)
        {
            if (ModelState.IsValid)
            {
                newsEvent.CreatedOn = DateTime.Now.Date;
                _context.Add(newsEvent);
                await _context.SaveChangesAsync();
                if(Attachment.Count > 0)
                {
                    string MaxID = _context.NewsEvent.Max(a => a.NewsEventID).ToString();
                    var rootPath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\NewsEvent\\NE" + MaxID);                    
                    foreach (var file in Attachment)
                    {                        
                        string fileName = Path.GetFileName(file.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        fileName = "NewsEvent" + MaxID + fileName;
                        NewsEventPicture Obj = new NewsEventPicture();
                        Obj.NewsEventID = int.Parse(MaxID);
                        Obj.PicturePath = Path.Combine("/Documents/NewsEvent/NE"+MaxID, fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        _context.Add(Obj);
                    }
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Create));
            }
            return View(newsEvent);
        }
        [Authorize(Roles = "Website")]
        // GET: website/NewsEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsEvent = await _context.NewsEvent.FindAsync(id);
            if (newsEvent == null)
            {
                return NotFound();
            }
            ViewBag.pictures = await _context.NewsEventPicture.Where(a=>a.NewsEventID == id).ToListAsync();
            return View(newsEvent);
        }

        // POST: website/NewsEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsEventID,Heading,Description,OnDate,CreatedOn,Visibility")] NewsEvent newsEvent, List<IFormFile> Attachment)
        {
            if (id != newsEvent.NewsEventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsEvent);
                    if (Attachment.Count > 0)
                    {
                        string MaxID = _context.NewsEvent.Max(a => a.NewsEventID).ToString();
                        var rootPath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\Documents\\NewsEvent\\NE" + MaxID);
                        foreach (var file in Attachment)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            fileName = fileName.Replace("&", "n");
                            fileName = fileName.Replace(" ", "");
                            fileName = fileName.Replace("#", "H");
                            fileName = fileName.Replace("(", "");
                            fileName = fileName.Replace(")", "");
                            fileName = "NewsEvent" + MaxID + fileName;
                            NewsEventPicture Obj = new NewsEventPicture();
                            Obj.NewsEventID = int.Parse(MaxID);
                            Obj.PicturePath = Path.Combine("/Documents/NewsEvent/NE" + MaxID, fileName);//Server Path
                            string sPath = Path.Combine(rootPath);
                            if (!System.IO.Directory.Exists(sPath))
                            {
                                System.IO.Directory.CreateDirectory(sPath);
                            }
                            string FullPathWithFileName = Path.Combine(sPath, fileName);
                            using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            _context.Add(Obj);
                        }
                        await _context.SaveChangesAsync();
                    }                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsEventExists(newsEvent.NewsEventID))
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
            return View(newsEvent);
        }
        [Authorize(Roles = "Website")]
        // GET: website/NewsEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsEvent = await _context.NewsEvent
                .FirstOrDefaultAsync(m => m.NewsEventID == id);
            if (newsEvent == null)
            {
                return NotFound();
            }
            ViewBag.pictures = await _context.NewsEventPicture.Where(a => a.NewsEventID == id).ToListAsync();
            return View(newsEvent);
        }

        // POST: website/NewsEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsEvent = await _context.NewsEvent.FindAsync(id);
            _context.NewsEvent.Remove(newsEvent);
            var list = _context.NewsEventPicture.Where(a => a.NewsEventID == newsEvent.NewsEventID).ToList();
            foreach(var obj in list)
            {
                _context.NewsEventPicture.Remove(obj);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed2(int id, int picId)
        {
            var newsEventPic = await _context.NewsEventPicture.FindAsync(picId);
            _context.NewsEventPicture.Remove(newsEventPic);           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Delete),new {id});
        }
        [HttpPost, ActionName("Delete3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed3(int id, int picId)
        {
            var newsEventPic = await _context.NewsEventPicture.FindAsync(picId);
            _context.NewsEventPicture.Remove(newsEventPic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id });
        }
        private bool NewsEventExists(int id)
        {
            return _context.NewsEvent.Any(e => e.NewsEventID == id);
        }
    }
}
