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
    public class WhatNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WhatNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/WhatNews
        public async Task<IActionResult> Index()
        {
            return View(await _context.WhatNew.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.WhatNew.ToListAsync());
        }
        // GET: website/WhatNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whatNew = await _context.WhatNew
                .FirstOrDefaultAsync(m => m.WhatNewsID == id);
            if (whatNew == null)
            {
                return NotFound();
            }

            return View(whatNew);
        }
        [Authorize(Roles = "Website")]
        // GET: website/WhatNews/Create
        public IActionResult Create()
        {
            WhatNew Obj = new WhatNew();
            Obj.Visibility = true;
            Obj.OnDate = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/WhatNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WhatNewsID,Heading,PicturePath,Description,OnDate,CreatedOn,Visibility")] WhatNew whatNew, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\WhatNews\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "WhatNews" + randomNumber.ToString() + fileName;
                    whatNew.PicturePath = Path.Combine("/Documents/WhatNews/", fileName);//Server Path
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
                else
                {
                    whatNew.PicturePath = "/img/LIVESTOCKLogo.png";
                }
                whatNew.CreatedOn = DateTime.Now.Date;
                _context.Add(whatNew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(whatNew);
        }
        [Authorize(Roles = "Website")]
        // GET: website/WhatNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whatNew = await _context.WhatNew.FindAsync(id);
            if (whatNew == null)
            {
                return NotFound();
            }
            return View(whatNew);
        }

        // POST: website/WhatNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WhatNewsID,Heading,PicturePath,Description,OnDate,CreatedOn,Visibility")] WhatNew whatNew, IFormFile Attachment)
        {
            if (id != whatNew.WhatNewsID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\WhatNews\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "WhatNews" + randomNumber.ToString() + fileName;
                        whatNew.PicturePath = Path.Combine("/Documents/WhatNews/", fileName);//Server Path
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
                    _context.Update(whatNew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WhatNewExists(whatNew.WhatNewsID))
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
            return View(whatNew);
        }
        [Authorize(Roles = "Website")]
        // GET: website/WhatNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whatNew = await _context.WhatNew
                .FirstOrDefaultAsync(m => m.WhatNewsID == id);
            if (whatNew == null)
            {
                return NotFound();
            }

            return View(whatNew);
        }

        // POST: website/WhatNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var whatNew = await _context.WhatNew.FindAsync(id);
            _context.WhatNew.Remove(whatNew);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool WhatNewExists(int id)
        {
            return _context.WhatNew.Any(e => e.WhatNewsID == id);
        }
    }
}
