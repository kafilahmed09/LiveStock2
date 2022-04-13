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
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class CardsController :BaselineController
    {        
        public CardsController(ApplicationDbContext context) : base(context)
        {

        }

    // GET: website/Cards
    public async Task<IActionResult> Index()
        {
            return View(await _context.Card.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Card.ToListAsync());
        }
        // GET: website/Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Modes"] = Modes;
            ViewData["ILinks"] = ImportantLinks;
            ViewBag.TotalLinks = (ImportantLinks.Length) / 2;
            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.CardID == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: website/Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardID,Heading,Description,Detail,PicturePath,OnDate,CreatedOn,Visibility")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        [Authorize(Roles = "Website")]
        // GET: website/Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: website/Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardID,Heading,Description,Detail,PicturePath,OnDate,CreatedOn,Visibility")] Card card, IFormFile Attachment)
        {
            if (id != card.CardID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Card\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Card" + randomNumber.ToString() + fileName;
                        card.PicturePath = Path.Combine("/Documents/Card/", fileName);//Server Path
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
                        Image_resize(rootPath + fileName, rootPath + fileName.Replace(".", "1."), 1200, 1200);
                        card.PicturePath = card.PicturePath.Replace(".", "1.");
                    }
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.CardID))
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
            return View(card);
        }
        //[Authorize(Roles = "Website")]
        //// GET: website/Cards/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var card = await _context.Card
        //        .FirstOrDefaultAsync(m => m.CardID == id);
        //    if (card == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(card);
        //}

        //// POST: website/Cards/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var card = await _context.Card.FindAsync(id);
        //    _context.Card.Remove(card);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.CardID == id);
        }
        private void Image_resize(string input_Image_Path, string output_Image_Path, int width, int height)
        {
            const long quality = 50L;
            using (var image = new Bitmap(System.Drawing.Image.FromFile(input_Image_Path)))
            {
                if (image.Width > 1200)
                {
                    width = 1500;
                    height = 1200;
                }
                var resized_Bitmap = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized_Bitmap))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);
                    using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        resized_Bitmap.Save(output, codec, encoderParameters);
                    }
                }
            }
        }
    }
}
