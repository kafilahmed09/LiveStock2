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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class DGPRsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DGPRsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/DGPRs
        public async Task<IActionResult> Index()
        {
            return View(await _context.DGPR.ToListAsync());
        }
        public async Task<IActionResult> Index2()
        {
            return PartialView(await _context.DGPR.ToListAsync());
        }
        // GET: website/DGPRs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dGPR = await _context.DGPR
                .FirstOrDefaultAsync(m => m.DGPRID == id);
            if (dGPR == null)
            {
                return NotFound();
            }

            return View(dGPR);
        }
        [Authorize(Roles = "Website")]
        // GET: website/DGPRs/Create
        public IActionResult Create()
        {
            DGPR Obj = new DGPR();
            Obj.Visibility = true;
            Obj.OnDate = DateTime.Now.Date;
            return View(Obj);
        }

        // POST: website/DGPRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DGPRID,Heading,Description,OnDate,CreatedOn,Visibility")] DGPR dGPR, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                if (Attachment != null)
                {
                    var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\DGPR\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "DGPR" + randomNumber.ToString() + fileName;
                    dGPR.PicturePath = Path.Combine("/Documents/DGPR/", fileName);//Server Path
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
                    /*Image_resize(rootPath + fileName, rootPath + fileName.Replace(".", "1."), 1200, 800);
                    dGPR.PicturePath = dGPR.PicturePath.Replace(".", "1.");*/
                }                              
                dGPR.Visibility = true;
                dGPR.CreatedOn = DateTime.Now.Date;
                _context.Add(dGPR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(dGPR);
        }
        [Authorize(Roles = "Website")]
        // GET: website/DGPRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dGPR = await _context.DGPR.FindAsync(id);
            if (dGPR == null)
            {
                return NotFound();
            }
            return View(dGPR);
        }

        // POST: website/DGPRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DGPRID,Heading,Description,OnDate,CreatedOn,Visibility")] DGPR dGPR, IFormFile Attachment)
        {
            if (id != dGPR.DGPRID)
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
                            Directory.GetCurrentDirectory(), "wwwroot\\Documents\\DGPR\\");
                        string fileName = Path.GetFileName(Attachment.FileName);
                        fileName = fileName.Replace("&", "n");
                        fileName = fileName.Replace(" ", "");
                        fileName = fileName.Replace("#", "H");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(")", "");
                        Random random = new Random();
                        int randomNumber = random.Next(1, 1000);
                        fileName = "DGPR" + randomNumber.ToString() + fileName;
                        dGPR.PicturePath = Path.Combine("/Documents/DGPR/", fileName);//Server Path
                        string sPath = Path.Combine(rootPath);
                        if (!System.IO.Directory.Exists(sPath))
                        {
                            System.IO.Directory.CreateDirectory(sPath);
                        }
                        string FullPathWithFileName = Path.Combine(sPath, fileName);
                        using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                        {
                            await Attachment.CopyToAsync(stream);
                            /*Image_resize(rootPath + fileName, rootPath + fileName.Replace(".", "1."), 1200, 800);
                            dGPR.PicturePath = dGPR.PicturePath.Replace(".", "1.");*/
                        }                        
                    }
                    _context.Update(dGPR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DGPRExists(dGPR.DGPRID))
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
            return View(dGPR);
        }
        [Authorize(Roles = "Website")]
        // GET: website/DGPRs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dGPR = await _context.DGPR
                .FirstOrDefaultAsync(m => m.DGPRID == id);
            if (dGPR == null)
            {
                return NotFound();
            }

            return View(dGPR);
        }

        // POST: website/DGPRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dGPR = await _context.DGPR.FindAsync(id);
            _context.DGPR.Remove(dGPR);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DGPRExists(int id)
        {
            return _context.DGPR.Any(e => e.DGPRID == id);
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
