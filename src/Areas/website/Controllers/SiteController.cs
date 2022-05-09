using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class SiteController : BaselineController
    {        
        public SiteController(ApplicationDbContext context) : base(context)
        {
            
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            ViewBag.Card = await _context.Card.Where(a => a.Visibility == true).Take(4).ToListAsync();
            /*ViewBag.WhatNews = await _context.WhatNew.Where(a => a.Visibility == true).ToListAsync();*/
            ViewBag.Notifications = await _context.Notification.Where(a => a.Visibility == true && a.ShowOnSlider == true).Take(10).ToListAsync();
            ViewBag.NewsEvent = await _context.NewsEvent.Where(a => a.Visibility == true).Take(10).ToListAsync();
            ViewBag.FolderGallery = await _context.GalleryFolder.Where(a => a.Visibility == true).OrderByDescending(a=>a.GalleryFolderId).Take(12).ToListAsync();
            ViewBag.MainSlider = await _context.MainSlider.OrderBy(a => a.OrderSequence).Where(a => a.Visibility == true).Take(20).ToListAsync();
            ViewBag.DGPR = await _context.DGPR.Where(a => a.Visibility == true).Take(8).ToListAsync();
            ViewBag.Video = await _context.Video.Where(a => a.Visibility == true).Take(8).ToListAsync();

            return View();
        }
        public IActionResult Index2()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            ViewBag.Card = _context.Card.Where(a => a.Visibility == true).ToList();
            //ViewBag.WhatNews = _context.WhatNew.Where(a => a.Visibility == true).ToList();
            ViewBag.Notifications = _context.Notification.Where(a => a.Visibility == true && a.ShowOnSlider == true).TakeLast(10).ToList();
            ViewBag.NewsEvent = _context.NewsEvent.Where(a => a.Visibility == true).TakeLast(10).ToList();
            ViewBag.Gallery = _context.Gallery.Where(a => a.Visibility == true).ToList();
            ViewBag.MainSlider = _context.MainSlider.OrderBy(a => a.OrderSequence).Where(a => a.Visibility == true).TakeLast(20).ToList();
            ViewBag.DGPR = _context.DGPR.Where(a => a.Visibility == true).TakeLast(8).ToList();
            ViewBag.Video = _context.Video.Where(a => a.Visibility == true).TakeLast(8).ToList();
            return View();
        }
        public IActionResult AboutUs()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult DGAHP()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult ViewAlbum(int id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            var data = _context.Gallery.Where(a => a.GalleryFolderId == id).ToList();
            ViewBag.Album = _context.GalleryFolder.Find(id).FolderTitle;
            return View(data);
        }
        public IActionResult SecretaryMsg()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;

            var Obj = _context.Director.Where(a => a.DirectorID == 4).First();
            ViewData["Heading"] = Obj.HeadName;
            ViewData["Description"] = Obj.Description;
            return View();
        }
        public IActionResult MinisterMsg()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;

            var Obj = _context.Director.Where(a => a.DirectorID == 5).First();
            ViewData["Heading"] = Obj.HeadName;
            ViewData["Description"] = Obj.Description;
            return View();
        }        
        public IActionResult DGFFR()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult DGRS()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public async Task<IActionResult> ViewDirector(int Id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View(await _context.Director.FindAsync(Id));
        }
        public IActionResult LiveStock()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult AHS()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult Services()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult Ongoing()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult Upcoming()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult LPB()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult WhatNewsDetail(int id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            WhatNew Obj = _context.WhatNew.Find(id);
            return View(Obj);
        }
        public IActionResult VideoDetail(int id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            Video Obj = _context.Video.Find(id);
            return View(Obj);
        }
        public IActionResult DGPRDetail(int id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            DGPR Obj = _context.DGPR.Find(id);
            return View(Obj);
        }
        public async Task<IActionResult> NewsEventDetail(int id)
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            NewsEvent Obj = await _context.NewsEvent.FindAsync(id);
            ViewBag.pictures = _context.NewsEventPicture.Where(a => a.NewsEventID == id).ToList();
            return View(Obj);
        }
        public IActionResult Contact()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            var Obj = _context.ContactInfo.Find(1);            
            return View(Obj);
        }
        public IActionResult Covid()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
        public IActionResult Complaint()
        {
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            return View();
        }
    }
}