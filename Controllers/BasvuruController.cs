using System;
using System.Linq;
using System.Threading.Tasks;
using CouncilAtelier.Data;
using CouncilAtelier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Controllers
{
    public class BasvuruController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public BasvuruController(CouncilAtelierContext context)
        {
            _context = context;
        }

        // /Basvur?type=Program&ref=programlar
        [HttpGet("/Basvur")]
        public async Task<IActionResult> Index(string? type = null, string? @ref = null)
        {
            var model = new Basvuru();

            // Dropdown ön seçimi
            if (!string.IsNullOrWhiteSpace(type))
            {
                type = type.Trim();
                if (type.Equals("Program", StringComparison.OrdinalIgnoreCase)) model.Type = "Program";
                else if (type.Equals("Workshop", StringComparison.OrdinalIgnoreCase)) model.Type = "Workshop";
                else model.Type = "Belirsiz";
            }

            // Programlar ve Workshopları yükle (soft delete olanları hariç)
            var programlar = await _context.Programlar
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Title)
                .Select(p => new { p.Id, p.Title })
                .ToListAsync();

            var workshoplar = await _context.Workshoplar
                .Where(w => !w.IsDeleted)
                .OrderBy(w => w.Title)
                .Select(w => new { w.Id, w.Title })
                .ToListAsync();

            ViewBag.Programlar = programlar;
            ViewBag.Workshoplar = workshoplar;
            ViewBag.Ref = @ref;
            ViewBag.Success = false;

            return View(model);
        }

        [HttpPost("/Basvur")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Basvuru model, string? @ref = null)
        {
            // normalize
            model.FullName = (model.FullName ?? "").Trim();
            model.Email = (model.Email ?? "").Trim();
            model.Phone = (model.Phone ?? "").Trim();
            model.SelectedItem = (model.SelectedItem ?? "").Trim();
            model.Note = (model.Note ?? "").Trim();

            if (!ModelState.IsValid)
            {
                // Hata durumunda da programlar ve workshopları yükle
                var programlar = await _context.Programlar
                    .Where(p => !p.IsDeleted)
                    .OrderBy(p => p.Title)
                    .Select(p => new { p.Id, p.Title })
                    .ToListAsync();

                var workshoplar = await _context.Workshoplar
                    .Where(w => !w.IsDeleted)
                    .OrderBy(w => w.Title)
                    .Select(w => new { w.Id, w.Title })
                    .ToListAsync();

                ViewBag.Programlar = programlar;
                ViewBag.Workshoplar = workshoplar;
                ViewBag.Ref = @ref;
                ViewBag.Success = false;
                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;
            model.IsRead = false;

            _context.Basvurular.Add(model);
            await _context.SaveChangesAsync();

            // Başarı durumunda da yükle (aynı sayfada kalacak)
            var programlarSuccess = await _context.Programlar
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Title)
                .Select(p => new { p.Id, p.Title })
                .ToListAsync();

            var workshoplarSuccess = await _context.Workshoplar
                .Where(w => !w.IsDeleted)
                .OrderBy(w => w.Title)
                .Select(w => new { w.Id, w.Title })
                .ToListAsync();

            ViewBag.Programlar = programlarSuccess;
            ViewBag.Workshoplar = workshoplarSuccess;
            ViewBag.Ref = @ref;
            ViewBag.Success = true;

            // formu temiz göstermek istersen:
            return View(new Basvuru { Type = model.Type });
        }
    }
}