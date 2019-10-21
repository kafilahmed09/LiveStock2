using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Areas.Procurement.Models;
using BES.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BES.Areas.Procurement.Controllers
{
    [Area("Procurement")]
    public class LotItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/LotItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LotItem.Include(l => l.Lot).Include(l => l.Unit);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index2(int id)
        {            
            var applicationDbContext = _context.LotItem.Include(p => p.Lot).Include(a => a.Unit).Where(a => a.lotId == id).Where(a => a.lotId == id);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        // GET: Procurement/LotItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotItem = await _context.LotItem
                .Include(l => l.Lot)
                .Include(l => l.Unit)
                .FirstOrDefaultAsync(m => m.LotItemId == id);
            if (lotItem == null)
            {
                return NotFound();
            }

            return View(lotItem);
        }

        [Authorize(Roles = "Procurement")]
        // GET: Procurement/LotItems/Create
        public IActionResult Create(int id)
        {
            var ActivityID = _context.Lot.Include(a => a.Activity).Where(a => a.lotId == id).FirstOrDefault();
            ViewBag.AID = ActivityID.Activity.ActivityID;
            var result = _context.Lot
                .Where(a => a.lotId == id)
                   .Select(x => new
                   {
                       x.lotId,
                       lotDescription = "Lot No. " + x.lotno.ToString()
                   });
            ViewBag.LotNo = result.Select(a => a.lotDescription).FirstOrDefault();
            ViewBag.lotId = new SelectList(result, "lotId", "lotDescription");
            ViewBag.UnitId = new SelectList(_context.Unit, "UnitId", "Name");
            ViewBag.LId = id;
            ViewBag.LotTotalItems = _context.Lot.Where(a => a.lotId == id).Select(a => a.ItemTotal).FirstOrDefault();
            ViewBag.CurrentLotItems = _context.LotItem.Count(a => a.lotId == id);
            return View();
        }

        // POST: Procurement/LotItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("LotItemId,lotId,ItemName,Quantity,EstimatedUnitRate,ActualUnitRate,UnitId,Description")] LotItem lotItem, IEnumerable<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lotItem);
                await _context.SaveChangesAsync();
                IFormFile picture = null;                
                foreach (var file in images)
                {
                    LotItemImage ImgObj = new LotItemImage();
                    if (file != null && file.Length > 0)
                    {
                        picture = file;
                        var rootPath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Procurement\\");
                        string fileName = Path.GetFileName(picture.FileName);
                        fileName = fileName.Replace("&", "n");
                        string AName = _context.Lot.Include(a => a.Activity).Where(a => a.lotId == id).Select(a => a.Activity.Name).FirstOrDefault().ToString();
                        AName = AName.Replace("&", "n");
                        string ItemName = lotItem.ItemName.Replace("&", "n");
                        var PPName = _context.ProcurementPlan.Find(_context.Activity.Find(_context.Lot.Find(lotItem.lotId).ActivityID).ProcurementPlanID).Name;
                        ImgObj.ImagePath = Path.Combine("/Documents/Procurement/", PPName + "/" + "//" + AName + "//Lot//" + lotItem.lotId.ToString() + "//" + ItemName + "//" + fileName);//Server Path                
                        string sPath = Path.Combine(rootPath, PPName + "/" + AName + "//Lot//" + lotItem.lotId.ToString() + "//" + ItemName);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await picture.CopyToAsync(stream);
                        }                        
                        ImgObj.LotItemId = _context.LotItem.Max(a => a.LotItemId);
                        ImgObj.Visibility = true;
                        _context.LotItemImage.Add(ImgObj);                        
                    }
                }
                _context.SaveChanges();
                ViewBag.LId = lotItem.lotId;
                ViewBag.UnitId = new SelectList(_context.Unit, "UnitId", "Name", lotItem.UnitId);
                return RedirectToAction(nameof(Create), new { lotItem.lotId});
            }
            var ActivityID = _context.Lot.Include(a => a.Activity).Where(a => a.lotId == id).FirstOrDefault();
            ViewBag.AID = ActivityID.Activity.ActivityID;
            var result = _context.Lot
                .Where(a => a.lotId == id)
                   .Select(x => new
                   {
                       x.lotId,
                       lotDescription = "Lot No. " + x.lotno.ToString()
                   });
            ViewBag.LotNo = result.Select(a => a.lotDescription).FirstOrDefault();
            ViewBag.lotId = new SelectList(result, "lotId", "lotDescription", lotItem.lotId);
            ViewBag.UnitId = new SelectList(_context.Unit, "UnitId", "Name", lotItem.UnitId);
            ViewBag.LId = id;
            ViewBag.LotTotalItems = _context.Lot.Where(a => a.lotId == id).Select(a => a.ItemTotal).FirstOrDefault();
            ViewBag.CurrentLotItems = _context.LotItem.Count(a => a.lotId == id);
            return View(lotItem);
        }

        [Authorize(Roles = "Procurement")]
        // GET: Procurement/LotItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotItem = await _context.LotItem.FindAsync(id);
            if (lotItem == null)
            {
                return NotFound();
            }
            var result = _context.Lot
                .Where(a => a.lotId == lotItem.lotId)
                   .Select(x => new
                   {
                       lotId = x.lotId,
                       lotDescription = "Lot No - " + x.lotno.ToString()
                   });
            ViewBag.UnitId = new SelectList(_context.Unit, "UnitId", "Name", lotItem.UnitId);
            ViewBag.lotId = new SelectList(result, "lotId", "lotDescription", lotItem.lotId);
            return View(lotItem);
        }

        // POST: Procurement/LotItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LotItemId,lotId,ItemName,Quantity,EstimatedUnitRate,ActualUnitRate,UnitId,Description")] LotItem lotItem, IEnumerable<IFormFile> images)
        {
            if (id != lotItem.LotItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lotItem);
                    await _context.SaveChangesAsync();
                    IFormFile picture = null;                   
                    foreach (var file in images)
                    {
                        LotItemImage ImgObj = new LotItemImage();
                        if (file != null && file.Length > 0)
                        {
                            picture = file;
                            var rootPath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Procurement\\");
                            string fileName = Path.GetFileName(picture.FileName);
                            fileName = fileName.Replace("&", "n");
                            string AName = _context.Lot.Include(a => a.Activity).Where(a => a.lotId == lotItem.lotId).Select(a => a.Activity.Name).FirstOrDefault().ToString();
                            AName = AName.Replace("&", "n");
                            string ItemName = lotItem.ItemName.Replace("&", "n");
                            var PPName = _context.ProcurementPlan.Find(_context.Activity.Find(_context.Lot.Find(lotItem.lotId).ActivityID).ProcurementPlanID).Name;
                            ImgObj.ImagePath = Path.Combine("/Documents/Procurement/", PPName + "/" + "//" + AName + "//Lot//" + lotItem.lotId.ToString() + "//" + ItemName + "//" + fileName);//Server Path                
                            string sPath = Path.Combine(rootPath, PPName + "/" + AName + "//Lot//" + lotItem.lotId.ToString() + "//" + ItemName);
                            if (!System.IO.Directory.Exists(sPath))
                            {
                                System.IO.Directory.CreateDirectory(sPath);
                            }
                            string FullPathWithFileName = Path.Combine(sPath, fileName);
                            using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                            {
                                await picture.CopyToAsync(stream);
                            }
                            ImgObj.LotItemId = lotItem.LotItemId;
                            ImgObj.Visibility = true;
                            _context.LotItemImage.Add(ImgObj);                            
                        }
                    }
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotItemExists(lotItem.LotItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create),new { id = lotItem.lotId});
            }
            var result = _context.Lot
               .Where(a => a.lotId == lotItem.lotId)
                  .Select(x => new
                  {
                      x.lotId,
                      lotDescription = "Lot No - " + x.lotno.ToString()
                  });
            ViewBag.UnitId = new SelectList(_context.Unit, "UnitId", "Name", lotItem.UnitId);
            ViewBag.lotId = new SelectList(result, "lotId", "lotDescription", lotItem.lotId);
            return View(lotItem);
        }

        // GET: Procurement/LotItems/Edit/5
        public async Task<IActionResult> EditPopup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotItemList = _context.LotItem.Where(a=>a.lotId == id);
            if (lotItemList == null)
            {
                return NotFound();
            }          
            return PartialView( await lotItemList.ToListAsync());
        }

        // POST: Procurement/LotItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPopup(int id, [Bind("LotItemId,lotId,ItemName,Quantity,EstimatedUnitRate,ActualUnitRate,UnitId,Description")] LotItem lotItem)
        {
            if (id != lotItem.LotItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lotItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotItemExists(lotItem.LotItemId))
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
            ViewData["lotId"] = new SelectList(_context.Lot, "lotId", "lotId", lotItem.lotId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "UnitId", "UnitId", lotItem.UnitId);
            return PartialView(lotItem);
        }
        // GET: Procurement/LotItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lotItem = await _context.LotItem
                .Include(l => l.Lot)
                .Include(l => l.Unit)
                .FirstOrDefaultAsync(m => m.LotItemId == id);
            if (lotItem == null)
            {
                return NotFound();
            }

            return View(lotItem);
        }

        // POST: Procurement/LotItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lotItem = await _context.LotItem.FindAsync(id);
            _context.LotItem.Remove(lotItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotItemExists(int id)
        {
            return _context.LotItem.Any(e => e.LotItemId == id);
        }
    }
}
