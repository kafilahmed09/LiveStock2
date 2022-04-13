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
    public class ContactInfoesController : BaselineController
    {        
        public ContactInfoesController(ApplicationDbContext context) : base(context)
        {            
        }               
        // GET: website/ContactInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactInfo.ToListAsync());
        }
        [Authorize(Roles = "Website")]
        // GET: website/ContactInfoes/Edit/5
        public async Task<IActionResult> Edit()
        {            

            var contactInfo = await _context.ContactInfo.FindAsync(1);
            if (contactInfo == null)
            {
                return NotFound();
            }            
            return View(contactInfo);
        }
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("ContactInfoID,PhoneNo1,PhoneNo2,PhoneNo3,PhoneNo4,FaxNo1,FaxNo2,FaxNo3,Address,Email,WhatsAppNo")] ContactInfo contactInfo)
        {           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInfoExists(contactInfo.ContactInfoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "ContactInfoes");
            }
            return View(contactInfo);
        }
       
        private bool ContactInfoExists(int id)
        {
            return _context.ContactInfo.Any(e => e.ContactInfoID == id);
        }
    }
}
