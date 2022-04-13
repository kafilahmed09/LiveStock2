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
    public class SocialMediasController : BaselineController
    {        
        public SocialMediasController(ApplicationDbContext context) : base(context)
        {
            ViewData["Modes"] = Modes;
        }        
        // GET: website/SocialMedias
        public async Task<IActionResult> Index()
        {
            return View(await _context.SocialMedia.ToListAsync());
        }
        [Authorize(Roles = "Website")]
        // GET: website/SocialMedias/Edit/5
        public async Task<IActionResult> Edit()
        {            
            var socialMedia = await _context.SocialMedia.ToListAsync();
            if (socialMedia == null)
            {
                return NotFound();
            }
            return View(socialMedia);
        }
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<SocialMedia> socialMedias)
        {           
            if (ModelState.IsValid)
            {                
                foreach (var social in socialMedias)
                {
                    try
                    {                        
                        _context.Update(social);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SocialMediaExists(social.SocialMediaID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"WebAdmin");
            }
            return View(socialMedias);
        }
      
        private bool SocialMediaExists(int id)
        {
            return _context.SocialMedia.Any(e => e.SocialMediaID == id);
        }
    }
}
