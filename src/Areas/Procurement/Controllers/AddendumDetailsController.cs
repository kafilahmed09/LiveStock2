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
    public class AddendumDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddendumDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Procurement/AddendumDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AddendumDetail.Include(a => a.Addendum).Include(a => a.LotItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Procurement/AddendumDetails
        public async Task<IActionResult> Index2(int id)
        {
            var applicationDbContext = _context.AddendumDetail.Include(a => a.Addendum).Include(a => a.LotItem).Where(a=>a.AddendumId == id);
            return PartialView(await applicationDbContext.ToListAsync());
        }
        // GET: Procurement/AddendumDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumDetail = await _context.AddendumDetail
                .Include(a => a.Addendum)
                .Include(a => a.LotItem)
                .FirstOrDefaultAsync(m => m.AddendumDetailId == id);
            if (addendumDetail == null)
            {
                return NotFound();
            }

            return View(addendumDetail);
        }

        [Authorize(Roles = "Procurement")]
        public IActionResult AddAddendumItems(int id)
        {            

            var LId = _context.Addendum.Include(a => a.Lot).Where(a => a.AddendumId == id).Select(a => a.LotId).FirstOrDefault();
            ViewBag.LId = LId;
            var result = _context.Lot
                .Where(a => a.lotId == LId)
                   .Select(x => new
                   {
                       x.lotId,
                       lotDescription = "Lot No - " + x.lotno.ToString()
                   }).OrderBy(a => a.lotId);
            ViewBag.LotId = new SelectList(result, "lotId", "lotDescription");
            var names = _context.AddendumDetail.Include(a => a.Addendum).Where(a => a.Addendum.LotId == (result.Select(b => b.lotId).FirstOrDefault())).Select(a => a.LotItemId).ToArray<int>();

            var ItemsList = from item in _context.LotItem
                            where item.lotId == (result.Select(b => b.lotId).FirstOrDefault()) && !names.Contains(item.LotItemId)
                            select item;
            ViewBag.AddID = id;
            ViewBag.PPLotItemId = new SelectList(ItemsList, "LotItemId", "ItemName");
            AddendumDetail Obj = new AddendumDetail();
            Obj.AddendumId = id;
            return View(Obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAddendumItems(int id, [Bind("AddendumDetailId,AddendumId,LotItemId,Quantity")] AddendumDetail addendumDetail)
        {
            if (ModelState.IsValid)
            {
                addendumDetail.AddendumId = id;
                _context.Add(addendumDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AddAddendumItems), new {id});
            }
            var LId = _context.Addendum.Include(a => a.Lot).Where(a => a.AddendumId == id).Select(a => a.LotId).FirstOrDefault();
            ViewBag.LId = LId;
            var result = _context.Lot
                .Where(a => a.lotId == LId)
                   .Select(x => new
                   {
                       x.lotId,
                       lotDescription = "Lot No - " + x.lotno.ToString()
                   }).OrderBy(a => a.lotId);
            ViewBag.LotId = new SelectList(result, "lotId", "lotDescription");
            var names = _context.AddendumDetail.Include(a => a.Addendum).Where(a => a.Addendum.LotId == (result.Select(b => b.lotId).FirstOrDefault())).Select(a => a.LotItemId).ToArray<int>();

            var ItemsList = from item in _context.LotItem
                            where item.lotId == (result.Select(b => b.lotId).FirstOrDefault()) && !names.Contains(item.LotItemId)
                            select item;
            ViewBag.AddID = id;
            ViewBag.PPLotItemId = new SelectList(ItemsList, "LotItemId", "ItemName");
            return View(addendumDetail);
        }

        [Authorize(Roles = "Procurement")]
        // GET: Procurement/AddendumDetails/Create
        public IActionResult Create()
        {
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId");
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId");
            return View();
        }

        // POST: Procurement/AddendumDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddendumDetailId,AddendumId,LotItemId,Quantity")] AddendumDetail addendumDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addendumDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId", addendumDetail.AddendumId);
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", addendumDetail.LotItemId);
            return View(addendumDetail);
        }

        [Authorize(Roles = "Procurement")]
        // GET: Procurement/AddendumDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumDetail = await _context.AddendumDetail.FindAsync(id);
            ViewBag.ItemName = _context.LotItem.Find(addendumDetail.LotItemId).ItemName;
            if (addendumDetail == null)
            {
                return NotFound();
            }                  
            return View(addendumDetail);
        }

        // POST: Procurement/AddendumDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddendumDetailId,AddendumId,LotItemId,Quantity")] AddendumDetail addendumDetail)
        {
            if (id != addendumDetail.AddendumDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addendumDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddendumDetailExists(addendumDetail.AddendumDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AddAddendumItems), new { id = addendumDetail.AddendumId});
            }
            ViewData["AddendumId"] = new SelectList(_context.Addendum, "AddendumId", "AddendumId", addendumDetail.AddendumId);
            ViewData["LotItemId"] = new SelectList(_context.LotItem, "LotItemId", "LotItemId", addendumDetail.LotItemId);
            return View(addendumDetail);
        }

        // GET: Procurement/AddendumDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addendumDetail = await _context.AddendumDetail
                .Include(a => a.Addendum)
                .Include(a => a.LotItem)
                .FirstOrDefaultAsync(m => m.AddendumDetailId == id);
            if (addendumDetail == null)
            {
                return NotFound();
            }

            return View(addendumDetail);
        }

        // POST: Procurement/AddendumDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addendumDetail = await _context.AddendumDetail.FindAsync(id);
            _context.AddendumDetail.Remove(addendumDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddendumDetailExists(int id)
        {
            return _context.AddendumDetail.Any(e => e.AddendumDetailId == id);
        }
    }
}
