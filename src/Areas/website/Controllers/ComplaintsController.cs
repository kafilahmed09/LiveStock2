using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using System.Net.Mail;
using System.Net;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class ComplaintsController : BaselineController
    {        
        public ComplaintsController(ApplicationDbContext context) : base(context)
        {            
        }               
        // GET: website/Complaints
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            return View(await _context.Complaint.ToListAsync());
        }

        // GET: website/Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.ComplaintID == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: website/Complaints/Create
        public IActionResult Create(short? id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            if (id == 1)
            {
                ViewBag.Val = 1;
            }
            else
            {
                ViewBag.Val = 0;
            }
            return View();
        }

        // POST: website/Complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintID,Name,CNIC,MobileNo,Subject,Description,Status,CreatedDate")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                complaint.CreatedDate = DateTime.Now.Date;
                complaint.Status = true;
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                SendEmail(complaint.Subject, "<b> Name: </b>" + complaint.Name+ "<br><b> CNIC: </b>" + complaint.CNIC+ "<br><b> Contact# </b>" + complaint.MobileNo + "<br><br><br>" + complaint.Description);
                return RedirectToAction(nameof(Create), new {id = 1 });
            }
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(complaint);
        }

        // GET: website/Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            return View(complaint);
        }

        // POST: website/Complaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplaintID,Name,CNIC,MobileNo,Subject,Description,Status,CreatedDate")] Complaint complaint)
        {
            if (id != complaint.ComplaintID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.ComplaintID))
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
            return View(complaint);
        }

        // GET: website/Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.ComplaintID == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: website/Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaint = await _context.Complaint.FindAsync(id);
            _context.Complaint.Remove(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaint.Any(e => e.ComplaintID == id);
        }

        private bool SendEmail(string sub, string text)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress("LIVESTOCK.general@gmail.com", "To LIVESTOCK"));
                message.From = new MailAddress("LIVESTOCK.general@gmail.com", "From LIVESTOCK");
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                message.Subject = sub;
                message.Body = text;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("LIVESTOCK.general@gmail.com", "03337839803");
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            return true;
        }
    }
}
