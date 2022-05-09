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

namespace LIVESTOCK.Areas.website.Controllers
{
    [Area("website")]
    public class GalleryFoldersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryFoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: website/GalleryFolders
        public async Task<IActionResult> Index()
        {
            return View(await _context.GalleryFolder.ToListAsync());
        }
        public IActionResult Index2()
        {
            var list = _context.GalleryFolder.ToList();           
            return PartialView(list);
        }
        // GET: website/GalleryFolders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryFolder = await _context.GalleryFolder
                .FirstOrDefaultAsync(m => m.GalleryFolderId == id);
            if (galleryFolder == null)
            {
                return NotFound();
            }

            return View(galleryFolder);
        }
        public Image CreateThumbnailImage(string fileName, int width = 64, int height = 88)
        {
            Image image = Image.FromFile(fileName);
            Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            image.Dispose();
            return thumb;
        }
        // GET: website/GalleryFolders/Create
        public IActionResult Create()
        {
            GalleryFolder gallery = new GalleryFolder();
            gallery.Visibility = true;
            return View(gallery);
        }

        // POST: website/GalleryFolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryFolderId,FolderTitle,FolderImagePath,ImageCount,CreatedOn,Visibility")] GalleryFolder galleryFolder, IFormFile Attachment)
        {
            if (ModelState.IsValid)
            {
                int counter = _context.GalleryFolder.Count();                
                if (counter > 40)
                {
                    ViewBag.Msg = "Total Album excceded limit of 10. Please first remove some Album.";
                    return View(galleryFolder);
                }
                else
                {
                    ViewBag.Msg = "";
                }                
                                
                if (Attachment != null && Attachment.Length > 0)
                {                    
                    var rootPath = Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot\\Documents\\FolderGallery\\");
                    string fileName = Path.GetFileName(Attachment.FileName);
                    fileName = fileName.Replace("&", "n");
                    fileName = fileName.Replace(" ", "");
                    //fileName = fileName.Replace(".", "-");
                    fileName = fileName.Replace("#", "H");
                    fileName = fileName.Replace("(", "");
                    fileName = fileName.Replace(")", "");
                    Random random = new Random();
                    int randomNumber = random.Next(1, 1000);
                    fileName = "FolderGallery" + randomNumber.ToString() + fileName;
                    galleryFolder.FolderImagePath = Path.Combine("/Documents/FolderGallery/", fileName);//Server Path
                    galleryFolder.ImageCount = 0;                    
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
                    System.Drawing.Image thumbnail = CreateThumbnailImage(sPath + fileName, 300, 250);
                    thumbnail.Save(sPath + fileName);
                    /*string extension = Path.GetExtension(fileName);
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
                            galleryFolder.FolderImagePath = Path.Combine("/Documents/FolderGallery/", fileName.Replace(extension, ".jpeg"));//Server Path
                        }
                        catch(Exception ex)
                        {

                        }                            
                    }*/
                    galleryFolder.CreatedOn = DateTime.Now.Date;
                    _context.Add(galleryFolder);                                        
                }
                await _context.SaveChangesAsync();                
                return RedirectToAction(nameof(Create));
            }
            return View(galleryFolder);
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
        // GET: website/GalleryFolders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryFolder = await _context.GalleryFolder.FindAsync(id);
            if (galleryFolder == null)
            {
                return NotFound();
            }
            return View(galleryFolder);
        }

        // POST: website/GalleryFolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryFolderId,FolderTitle,FolderImagePath,ImageCount,CreatedOn,Visibility")] GalleryFolder galleryFolder)
        {
            if (id != galleryFolder.GalleryFolderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryFolder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryFolderExists(galleryFolder.GalleryFolderId))
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
            return View(galleryFolder);
        }
        [HttpPost, ActionName("Delete3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed3(int id)
        {
            var gallery = await _context.GalleryFolder.FindAsync(id);
            _context.GalleryFolder.Remove(gallery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
        // GET: website/GalleryFolders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryFolder = await _context.GalleryFolder
                .FirstOrDefaultAsync(m => m.GalleryFolderId == id);
            if (galleryFolder == null)
            {
                return NotFound();
            }

            return View(galleryFolder);
        }

        // POST: website/GalleryFolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryFolder = await _context.GalleryFolder.FindAsync(id);
            _context.GalleryFolder.Remove(galleryFolder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryFolderExists(int id)
        {
            return _context.GalleryFolder.Any(e => e.GalleryFolderId == id);
        }
    }
}
