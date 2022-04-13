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
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class GalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/Galleries
        public async Task<IActionResult> Index()
        {
            var list = await _context.Gallery.ToListAsync();                        
            return View(list);
        }
        public IActionResult Index2(int galleryFolderId)
        {
            var list = _context.Gallery.Where(a=>a.GalleryFolderId == galleryFolderId).ToList();
            //foreach (var obj in list)
            //{
            //    var img = Image.FromFile(obj.PicturePath);
            //    var scaleImg = ImageResize.Scale(img, 350, 280);
            //    obj.PicturePath = obj.PicturePath.Replace(".", "1.");
            //    scaleImg.SaveAs(obj.PicturePath);
            //}

            return PartialView(list);
        }
        // GET: website/Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery
                .FirstOrDefaultAsync(m => m.GalleryID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }
        [Area("Website")]
        // GET: website/Galleries/Create
        public IActionResult Create(int? id)
        {
            if(id == null)
            {
                ViewData["GalleryFolderId"] = new SelectList(_context.GalleryFolder, "GalleryFolderId", "FolderTitle");
            }
            else
            {
                ViewData["GalleryFolderId"] = new SelectList(_context.GalleryFolder, "GalleryFolderId", "FolderTitle", id);
            }
            return View();
        }

        // POST: website/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Website")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryID,PicturePath,GalleryFolderId,CreatedOn,Visibility")] Gallery gallery,IEnumerable<IFormFile> Attachment, int gallaryFolderId)
        {
            if (ModelState.IsValid)
            {
                int counter = _context.Gallery.Where(a=>a.GalleryFolderId == gallery.GalleryFolderId).Count();
                counter = counter + Attachment.Count();
                if (counter > 50)
                {
                    ViewBag.Msg = "Total Images excceded limit of 50 images. Please first remove some images.";
                    return View(gallery);
                }
                else
                {
                    ViewBag.Msg = "";
                }
                IFormFile picture = null;
                foreach (var file in Attachment)
                {
                    Gallery ImgObj = new Gallery();
                    if (file != null && file.Length > 0)
                    {
                        picture = file;
                        var rootPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Documents\\Gallery\\");
                    string fileName = Path.GetFileName(picture.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    //fileName = fileName.Replace(".", "-");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "Gallery" + randomNumber.ToString() + fileName;
                    ImgObj.PicturePath = Path.Combine("/Documents/Gallery/", fileName);//Server Path
                    ImgObj.GalleryFolderId = gallery.GalleryFolderId;
                    string sPath = Path.Combine(rootPath);
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    string FullPathWithFileName = Path.Combine(sPath, fileName);
                    using (var stream = new FileStream(FullPathWithFileName, FileMode.Create))
                    {
                        await picture.CopyToAsync(stream);
                    }
                    string extension = Path.GetExtension(fileName);
                    if (extension != ".jpeg" || extension != ".jpg")
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(sPath + fileName);
                        var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        var qualityEncoder = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityEncoder, 50L);
                        try
                        {
                            image.Save(sPath + fileName.Replace(extension, ".jpeg"), jpgEncoder, encoderParameters);
                            ImgObj.PicturePath = Path.Combine("/Documents/Gallery/", fileName.Replace(extension, ".jpeg"));//Server Path
                        }
                        catch (Exception ex)
                        {

                        }
                    }                        
                       ImgObj.Visibility = true;
                    ImgObj.CreatedOn = DateTime.Now.Date;
                    _context.Add(ImgObj);
                        counter++;
                        if (counter > 50)
                            break;
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Create), new { id = gallery.GalleryFolderId});
            }
            ViewData["GalleryFolderId"] = new SelectList(_context.GalleryFolder, "GalleryFolderId", "FolderTitle", gallaryFolderId);
            return View(gallery);
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
        [Authorize(Roles = "Website")]
        // GET: website/Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gallery = await _context.Gallery
                .FirstOrDefaultAsync(m => m.GalleryID == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: website/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gallery = await _context.Gallery.FindAsync(id);
            _context.Gallery.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed3(int id)
        {
            var gallery = await _context.Gallery.FindAsync(id);
            _context.Gallery.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        private bool GalleryExists(int id)
        {
            return _context.Gallery.Any(e => e.GalleryID == id);
        }

        private void Image_resize(string input_Image_Path, string output_Image_Path, int width,int height)
        {           
            const long quality = 50L;
            using (var image = new Bitmap(System.Drawing.Image.FromFile(input_Image_Path)))
            {               
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
