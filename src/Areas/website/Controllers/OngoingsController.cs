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
    public class OngoingsController : BaselineController
    {        
        public OngoingsController(ApplicationDbContext context) : base(context)
        {            
        }

        // GET: website/Ongoings
        public async Task<IActionResult> Index(int id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            if(id == 1)
            {
                ViewBag.Projects = "Ongoing Development Projects";
            }
            else if(id == 2)
            {
                ViewBag.Projects = "New Schemes Development Projects";
            }
            else
            {
                ViewBag.Projects = "On Board (Newly) Development Projects";
            }
            return View(await _context.Ongoing.Include(a=>a.Director).Where(a=>a.Status == id).ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Ongoing.OrderBy(a=>a.DirectorID).ToListAsync());
        }
        // GET: website/Ongoings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ongoing = await _context.Ongoing
                .FirstOrDefaultAsync(m => m.OngoingID == id);
            if (ongoing == null)
            {
                return NotFound();
            }

            return View(ongoing);
        }

        // GET: website/Ongoings/Create
        [Authorize(Roles = "Website")]
        public IActionResult Create()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="On going", Value = "1" , Selected = true },
                new SelectListItem{ Text="Up Coming", Value = "2" },
                new SelectListItem{ Text="On Board", Value = "3"},
            };
            ViewData["DirectorID"] = new SelectList(_context.Director.Where(a=>a.DirectorID <= 3), "DirectorID", "Department");
            ViewData["ddList"] = list;
            Ongoing Obj = new Ongoing();
            Obj.Status = 1;
            Obj.CreatedOn = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/Ongoings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OngoingID,SerialNo,ProjectID,PSDP,Name,EstimatedCost,Exp,Fin,Allocation,FinTar,Thr,DirectorID,Status,CreatedOn")] Ongoing ongoing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ongoing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="On going", Value = "1" , Selected = true },
                new SelectListItem{ Text="Up Coming", Value = "2" },
                new SelectListItem{ Text="On Board", Value = "3"},
            };            
            ViewData["ddList"] = list;
            ViewData["DirectorID"] = new SelectList(_context.Director.Where(a=>a.DirectorID <= 3), "DirectorID", "Department", ongoing.DirectorID);
            return View(ongoing);
        }

        [Authorize(Roles = "Website")]
        // GET: website/Ongoings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ongoing = await _context.Ongoing.FindAsync(id);
            if (ongoing == null)
            {
                return NotFound();
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="On going", Value = "1" , Selected = true },
                new SelectListItem{ Text="Up Coming", Value = "2" },
                new SelectListItem{ Text="On Board", Value = "3"},
            };
            ViewData["DirectorID"] = new SelectList(_context.Director.Where(a=>a.DirectorID <= 3), "DirectorID", "Department", ongoing.DirectorID);
            return View(ongoing);
        }

        // POST: website/Ongoings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OngoingID,SerialNo,ProjectID,PSDP,Name,EstimatedCost,Exp,Fin,Allocation,FinTar,Thr,DirectorID,Status,CreatedOn")] Ongoing ongoing)
        {
            if (id != ongoing.OngoingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ongoing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OngoingExists(ongoing.OngoingID))
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
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="On going", Value = "1" , Selected = true },
                new SelectListItem{ Text="Up Coming", Value = "2" },
                new SelectListItem{ Text="On Board", Value = "3"},
            };
            ViewData["DirectorID"] = new SelectList(_context.Director.Where(a=>a.DirectorID <= 3), "DirectorID", "Department", ongoing.DirectorID);
            return View(ongoing);
        }

        [Authorize(Roles = "Website")]
        // GET: website/Ongoings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ongoing = await _context.Ongoing
                .FirstOrDefaultAsync(m => m.OngoingID == id);
            if (ongoing == null)
            {
                return NotFound();
            }

            return View(ongoing);
        }

        // POST: website/Ongoings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ongoing = await _context.Ongoing.FindAsync(id);
            _context.Ongoing.Remove(ongoing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OngoingExists(int id)
        {
            return _context.Ongoing.Any(e => e.OngoingID == id);
        }
    }
}
