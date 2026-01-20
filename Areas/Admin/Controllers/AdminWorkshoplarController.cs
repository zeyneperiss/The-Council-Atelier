using CouncilAtelier.Data;
using CouncilAtelier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AdminWorkshoplarController : Controller
    {
        private readonly CouncilAtelierContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminWorkshoplarController(CouncilAtelierContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ Aktif (silinmemiş) workshoplar
        public async Task<IActionResult> Index()
        {
            var workshops = await _context.Workshoplar
                .Include(w => w.Category)
                .Where(w => !w.IsDeleted)
                .OrderByDescending(w => w.Id)
                .ToListAsync();

            return View(workshops);
        }

        // ✅ Son Silinenler
        public async Task<IActionResult> Deleted()
        {
            var deleted = await _context.Workshoplar
                .IgnoreQueryFilters()
                .Include(w => w.Category)
                .Where(w => w.IsDeleted)
                .OrderByDescending(w => w.DeletedAt)
                .ToListAsync();

            return View(deleted);
        }

        // ✅ Create (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(new Workshoplar());
        }

        // ✅ Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Workshoplar model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
                return View(model);
            }

            // Görsel yükleme
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                model.ImagePath = uniqueFileName;
            }

            model.IsDeleted = false;
            model.DeletedAt = null;

            _context.Workshoplar.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ✅ Edit (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var workshop = await _context.Workshoplar
                .Include(w => w.Category)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null) return NotFound();

            ViewBag.Categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(workshop);
        }

        // ✅ Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Workshoplar model, IFormFile? imageFile)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
                return View(model);
            }

            var workshop = await _context.Workshoplar
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null) return NotFound();

            // Görsel yükleme
            if (imageFile != null && imageFile.Length > 0)
            {
                // Eski görseli sil
                if (!string.IsNullOrEmpty(workshop.ImagePath))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, "images", workshop.ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni görseli yükle
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                workshop.ImagePath = uniqueFileName;
            }

            // güvenli update
            workshop.Title = model.Title;
            workshop.Description = model.Description;
            workshop.CategoryId = model.CategoryId;
            workshop.EventDate = model.EventDate;
            workshop.Location = model.Location;
            workshop.Capacity = model.Capacity;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ Soft Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var workshop = await _context.Workshoplar
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null) return NotFound();

            workshop.IsDeleted = true;
            workshop.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ Restore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var workshop = await _context.Workshoplar
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workshop == null) return NotFound();

            workshop.IsDeleted = false;
            workshop.DeletedAt = null;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deleted));
        }
    }
}