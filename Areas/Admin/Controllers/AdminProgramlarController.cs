using CouncilAtelier.Data;
using CouncilAtelier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class AdminProgramlarController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public AdminProgramlarController(CouncilAtelierContext context)
        {
            _context = context;
        }

        // Liste
        public async Task<IActionResult> Index()
        {
            var list = await _context.Programlar
                .Include(p => p.Category)
                .OrderBy(p => p.CategoryId)
                .ThenBy(p => p.Id)
                .ToListAsync();

            return View(list);
        }

        // Create (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillCategories();
            return View(new Programlar());
        }

        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Programlar model)
        {
            if (!ModelState.IsValid)
            {
                await FillCategories();
                return View(model);
            }

            _context.Programlar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Edit (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Programlar.FindAsync(id);
            if (item == null) return NotFound();

            await FillCategories(item.CategoryId);
            return View(item);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Programlar model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await FillCategories(model.CategoryId);
                return View(model);
            }

            var item = await _context.Programlar.FindAsync(id);
            if (item == null) return NotFound();

            item.Title = model.Title;
            item.Description = model.Description;
            item.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Delete (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var program = await _context.Programlar.FirstOrDefaultAsync(p => p.Id == id);
            if (program == null) return NotFound();

            program.IsDeleted = true;
            program.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private async Task FillCategories(int? selectedId = null)
        {
            var cats = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Categories = new SelectList(cats, "Id", "Name", selectedId);
        }

        public async Task<IActionResult> Deleted()
        {
            var deleted = await _context.Programlar
                .IgnoreQueryFilters()
                .Include(p => p.Category)
                .Where(p => p.IsDeleted)
                .OrderByDescending(p => p.DeletedAt)
                .ToListAsync();

            return View(deleted);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var program = await _context.Programlar
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (program == null) return NotFound();

            program.IsDeleted = false;
            program.DeletedAt = null;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deleted));
        }


    }
}