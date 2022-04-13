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
    public class BudgetsController : BaselineController
    {        
        public BudgetsController(ApplicationDbContext context)  : base(context)
        {            
        }

        // GET: website/Budgets
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.Budget.OrderByDescending(a=>a.Year).OrderBy(a=>a.Quarter).ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Budget.OrderByDescending(a => a.Year).OrderBy(a => a.Quarter).ToListAsync());
        }
        // GET: website/Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget
                .FirstOrDefaultAsync(m => m.BudgetID == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: website/Budgets/Create
        [Authorize(Roles = "Website")]
        public IActionResult Create()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="First", Value = "1" , Selected = true },
                new SelectListItem{ Text="Second", Value = "2" },
                new SelectListItem{ Text="Third", Value = "3"},
                new SelectListItem{ Text="Fourth", Value = "4"},
                new SelectListItem{ Text="Yearly", Value = "5"},
            };
            ViewData["ddQuarterList"] = list;
            ViewBag.YearsList = Enumerable.Range(2020, 30).Select(g => new SelectListItem { Value = g.ToString(), Text = (g.ToString() + " - " + (g+1).ToString())}).ToList();
            Budget Obj = new Budget();            
            Obj.CreatedOn = DateTime.Now.Date;
            return View(Obj);            
        }

        // POST: website/Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetID,Year,Quarter,filepath,Amount,CreatedOn")] Budget budget, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Budgets\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Notif" + randomNumber.ToString() + fileName;
                    budget.filepath = Path.Combine("/Documents/Budgets/", fileName);//Server Path
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
                _context.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="First", Value = "1" , Selected = true },
                new SelectListItem{ Text="Second", Value = "2" },
                new SelectListItem{ Text="Third", Value = "3"},
                new SelectListItem{ Text="Fourth", Value = "4"},
            };
            ViewData["ddQuarterList"] = list;
            ViewBag.YearsList = Enumerable.Range(2021, 30).Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() }).ToList();
            return View(budget);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="First", Value = "1" , Selected = true },
                new SelectListItem{ Text="Second", Value = "2" },
                new SelectListItem{ Text="Third", Value = "3"},
                new SelectListItem{ Text="Fourth", Value = "4"},
            };
            ViewData["ddQuarterList"] = list;
            ViewBag.YearsList = Enumerable.Range(2021, 30).Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() }).ToList();
            return View(budget);
        }

        // POST: website/Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetID,Year,Quarter,filepath,Amount,CreatedOn")] Budget budget, IFormFile Attachment)
        {
            if (id != budget.BudgetID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Budgets\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Notif" + randomNumber.ToString() + fileName;
                        budget.filepath = Path.Combine("/Documents/Budgets/", fileName);//Server Path
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
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.BudgetID))
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
                new SelectListItem{ Text="First", Value = "1" , Selected = true },
                new SelectListItem{ Text="Second", Value = "2" },
                new SelectListItem{ Text="Third", Value = "3"},
                new SelectListItem{ Text="Fourth", Value = "4"},
            };
            ViewData["ddQuarterList"] = list;
            ViewBag.YearsList = Enumerable.Range(2021, 30).Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() }).ToList();
            return View(budget);
        }

        // GET: website/Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget
                .FirstOrDefaultAsync(m => m.BudgetID == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: website/Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _context.Budget.FindAsync(id);
            _context.Budget.Remove(budget);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetExists(int id)
        {
            return _context.Budget.Any(e => e.BudgetID == id);
        }
    }
}
