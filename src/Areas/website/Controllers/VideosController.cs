using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIVESTOCK.Areas.website.Models;
using LIVESTOCK.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/Videos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Video.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.Video.ToListAsync());
        }
        // GET: website/Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Video
                .FirstOrDefaultAsync(m => m.VideoID == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Videos/Create
        public IActionResult Create()
        {
            Video Obj = new Video();
            Obj.Visibility = true;
            Obj.OnDate = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoID,Heading,Description,PicturePath,VideoLink,Duration,OnDate,CreatedOn,Visibility")] Video video, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Video\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Video" + randomNumber.ToString() + fileName;
                    video.PicturePath = Path.Combine("/Documents/Video/", fileName);//Server Path
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
                    Image_resize(rootPath + fileName, rootPath + fileName.Replace(".", "1."), 1200, 800);
                    video.PicturePath = video.PicturePath.Replace(".", "1.");
                }                
                video.Visibility = true;
                video.CreatedOn = DateTime.Now.Date;
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(video);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Video.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        // POST: website/Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoID,Heading,Description,PicturePath,VideoLink,Duration,OnDate,CreatedOn,Visibility")] Video video, IFormFile Attachment)
        {
            if (id != video.VideoID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Video\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "Video" + randomNumber.ToString() + fileName;
                        video.PicturePath = Path.Combine("/Documents/Video/", fileName);//Server Path
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
                        Image_resize(rootPath + fileName, rootPath + fileName.Replace(".", "1."), 1200, 800);
                        video.PicturePath = video.PicturePath.Replace(".", "1.");
                    }                    
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.VideoID))
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
            return View(video);
        }
        [Authorize(Roles = "Website")]
        // GET: website/Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Video
                .FirstOrDefaultAsync(m => m.VideoID == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: website/Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Video.FindAsync(id);
            _context.Video.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Video.Any(e => e.VideoID == id);
        }
        private void Image_resize(string input_Image_Path, string output_Image_Path, int width, int height)
        {
            const long quality = 50L;
            using (var image = new Bitmap(System.Drawing.Image.FromFile(input_Image_Path)))
            {
                if (image.Width > 1200)
                {
                    width = image.Width;
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
