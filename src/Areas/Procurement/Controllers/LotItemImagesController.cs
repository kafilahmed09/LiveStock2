using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Areas.Procurement.Models;
using BES.Data;
using Microsoft.AspNetCore.Authorization;

namespace BES.Areas.Procurement.Controllers
{
    [Area("Procurement")]
    public class LotItemImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotItemImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/LotItemImages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LotItemImage.Include(l => l.LotItem);
            return View(await applicationDbContext.ToListAsync());
        }
        public ActionResult ViewImages(int id)
        {
            List<LotItemImage> ItemImagesListArray = new List<LotItemImage>();
            ItemImagesListArray = _context.LotItemImage.Include(l => l.LotItem.Lot.Activity).Where(a=>a.LotItemId == id).ToList();
            return View(ItemImagesListArray);
        }
        // GET: Procurement/LotItemImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotItemImage = await _context.LotItemImage
                .Include(l => l.LotItem)
                .FirstOrDefaultAsync(m => m.ItemImageId == id);
            if (lotItemImage == null)
            {
                return NotFound();
            }

            return View(lotItemImage);
        }

        [Authorize(Roles = "Procurement")]
        // GET: Procurement/LotItemImages/Create
        public IActionResult Create()
        {
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId");
            return View();
        }

        // POST: Procurement/LotItemImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemImageId,LotItemId,ImagePath,Visibility")] LotItemImage lotItemImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lotItemImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", lotItemImage.LotItemId);
            return View(lotItemImage);
        }

        [Authorize(Roles = "Procurement")]
        // GET: Procurement/LotItemImages/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            List<LotItemImage> ItemImagesListArray = new List<LotItemImage>();
            ItemImagesListArray = await _context.LotItemImage.Include(p => p.LotItem.Lot.Activity).Where(a => a.LotItemId == id).ToListAsync();
            
            if (ItemImagesListArray == null)
            {
                return NotFound();
            }            
            return View(ItemImagesListArray);
        }

        // POST: Procurement/LotItemImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, List<LotItemImage> models, string[] ckboxarray)
        {           

            if (ModelState.IsValid)
            {
                foreach (var obj in models)
                {
                    if (ckboxarray.Contains(obj.ItemImageId.ToString()))
                    {
                        obj.Visibility = false;
                        obj.ImagePath = null;
                        obj.LotItem = null;
                        _context.Remove(obj);
                        await _context.SaveChangesAsync();
                    }
                }                
                return RedirectToAction(nameof(Edit), new { id });
            }            
            return View(models);
        }

        // GET: Procurement/LotItemImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotItemImage = await _context.LotItemImage
                .Include(l => l.LotItem)
                .FirstOrDefaultAsync(m => m.ItemImageId == id);
            if (lotItemImage == null)
            {
                return NotFound();
            }

            return View(lotItemImage);
        }

        // POST: Procurement/LotItemImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lotItemImage = await _context.LotItemImage.FindAsync(id);
            _context.LotItemImage.Remove(lotItemImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotItemImageExists(int id)
        {
            return _context.LotItemImage.Any(e => e.ItemImageId == id);
        }
    }
}
