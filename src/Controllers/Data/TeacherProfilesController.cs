using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BES.Data;
using BES.Models.Data;
using System.IO;

namespace BES.Controllers.Data
{
    public class TeacherProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeacherProfiles
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.SchoolID = id;
            var applicationDbContext = _context.TeacherProfile.Include(t => t.School).Include(t => t.TeacherPost)
                  .Where(t => t.SchoolID == id); ;
            return PartialView(await applicationDbContext.ToListAsync());
        }

        // GET: TeacherProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.TeacherProfile
                .Include(t => t.School)
                .Include(t => t.TeacherPost)
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }

            return View(teacherProfile);
        }

        // GET: TeacherProfiles/Create
        public IActionResult Create(int id)
        {
            TeacherProfile TeacherProfile = new TeacherProfile();
            TeacherProfile.SchoolID = id;
            TeacherProfile.School = _context.Schools.Find(id);

            ViewBag.SchoolID = new SelectList(_context.Schools, "SchoolID", "SName");
            //ViewBag.PostID = new SelectList(_context.TeacherPosts, "PostID", "PostName");
            ViewBag.NullableBool = new SelectList(new[]
                    {
                         new { Id = "", Name = "" },
                        new { Id = "True", Name = "Yes" },
                        new { Id = "False", Name = "No" }
                    }, "Id", "Name");
            ViewData["PostID"] = new SelectList(_context.Set<TeacherPost>(), "PostID", "PostName");
            return View(TeacherProfile);
        }

        // POST: TeacherProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,SchoolID,PostID,TName,FatherHusbandName,CNIC,IsFemale,IsProjectTeacher,ContractAward,ContractUrl,DateOfJoining,EcePreScore,EcePostScore,EstPreScore,EstPostScore,HTPreScore,HTPostScore,JoiningUrl")] TeacherProfile teacherProfile)
        {
            if (ModelState.IsValid)
            {
                if(teacherProfile.IsProjectTeacher==true)
                { teacherProfile.ContractAward =false; }
                _context.Add(teacherProfile);
                await _context.SaveChangesAsync();
                 //Save uploaded files
                var files = Request.Form.Files;
                if(!files.Any())
                {
                    return RedirectToAction("Update", "IncdicatorTrackings", new { id = teacherProfile.SchoolID, secID = "926982" });
                }
                string District = _context.Schools.Include(a => a.UC.Tehsil.District)
                                      .Where(a => a.SchoolID == teacherProfile.SchoolID)
                                    .Select(a => a.UC.Tehsil.District.DistrictName).FirstOrDefault();
                //string 
                var rootPath = Path.Combine(
                               Directory.GetCurrentDirectory(), "wwwroot\\Documents\\IndicatorEvidences\\");

                string sPath = Path.Combine(rootPath + District + "/" + "_TeachersContract" + "/", teacherProfile.SchoolID.ToString());
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                //short i = 1;
                string fileName = teacherProfile.TeacherID.ToString();
                foreach (var file in files)
                {
                    string FullPathWithFileName = Path.Combine(sPath, fileName + Path.GetExtension(file.FileName));
                    using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                }
                teacherProfile.ContractUrl= Path.Combine("/Documents/IndicatorEvidences/", District + "//" + "_TeachersContract" + "//" + teacherProfile.SchoolID+"//"+teacherProfile.TeacherID+".pdf");//Server Path
                teacherProfile.ContractAward = true;
                teacherProfile.ContractDate = DateTime.Now;
                _context.Update(teacherProfile);
                await _context.SaveChangesAsync();

                return RedirectToAction("Update", "IncdicatorTrackings", new { id = teacherProfile.SchoolID, secID= "926982" });

                }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", teacherProfile.SchoolID);
            ViewData["PostID"] = new SelectList(_context.Set<TeacherPost>(), "PostID", "PostID", teacherProfile.PostID);
            return View(teacherProfile);
        }

        // GET: TeacherProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var teacherProfile = await  _context.TeacherProfile.Include(a=>a.School).Where(a=>a.TeacherID==id).FirstOrDefaultAsync();

            //var teacherProfile = await  _context.TeacherProfile.Include(a=>a.School).Where(a=>a.SchoolID==id).FirstOrDefaultAsync();
            //if (teacherProfile == null)
            //{
            //    return NotFound();
            //}
            //ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", teacherProfile.SchoolID);
            ViewData["PostID"] = new SelectList(_context.Set<TeacherPost>(), "PostID", "PostName", teacherProfile.PostID);
            return View(teacherProfile);
        }

        // POST: TeacherProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("TeacherID,SchoolID,PostID,TName,FatherHusbandName,CNIC,IsFemale,IsProjectTeacher,ContractAward,ContractUrl,DateOfJoining,EcePreScore,EcePostScore,EstPreScore,EstPostScore,HTPreScore,HTPostScore,JoiningUrl")] TeacherProfile teacherProfile)
        {
            if (id != teacherProfile.TeacherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  
                    //Save uploaded files
                    var files = Request.Form.Files;
                    if (!files.Any())
                    {
                        return RedirectToAction("Update", "IncdicatorTrackings", new { id = teacherProfile.SchoolID, secID = "926982" });
                    }
                    string District = _context.Schools.Include(a => a.UC.Tehsil.District)
                                          .Where(a => a.SchoolID == teacherProfile.SchoolID)
                                        .Select(a => a.UC.Tehsil.District.DistrictName).FirstOrDefault();
                    //string 
                    var rootPath = Path.Combine(
                                   Directory.GetCurrentDirectory(), "wwwroot\\Documents\\IndicatorEvidences\\");

                    string sPath = Path.Combine(rootPath + District + "/" + "_TeachersContract" + "/", teacherProfile.SchoolID.ToString());
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    //short i = 1;
                    string fileName = teacherProfile.TeacherID.ToString();
                    foreach (var file in files)
                    {
                        string FullPathWithFileName = Path.Combine(sPath, fileName + Path.GetExtension(file.FileName));
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                    }
                    teacherProfile.ContractUrl = Path.Combine("/Documents/IndicatorEvidences/", District + "//" + "_TeachersContract" + "//" + teacherProfile.SchoolID + "//" + teacherProfile.TeacherID + ".pdf");//Server Path
                    teacherProfile.ContractAward = true;
                    teacherProfile.ContractDate = DateTime.Now;
                    _context.Update(teacherProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherProfileExists(teacherProfile.TeacherID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Update", "IncdicatorTrackings", new { id = teacherProfile.SchoolID, secID = "926982" });

            }
            ViewData["SchoolID"] = new SelectList(_context.Schools, "SchoolID", "SName", teacherProfile.SchoolID);
            ViewData["PostID"] = new SelectList(_context.Set<TeacherPost>(), "PostID", "PostName", teacherProfile.PostID);
            return View(teacherProfile);
        }

        // GET: TeacherProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.TeacherProfile
                .Include(t => t.School)
                .Include(t => t.TeacherPost)
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }

            return View(teacherProfile);
        }

        // POST: TeacherProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var teacherProfile = await _context.TeacherProfile.FindAsync(id);
            _context.TeacherProfile.Remove(teacherProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherProfileExists(int? id)
        {
            return _context.TeacherProfile.Any(e => e.TeacherID == id);
        }
    }
}
