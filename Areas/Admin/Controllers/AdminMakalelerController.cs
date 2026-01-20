using System;
using System.Linq;
using System.Threading.Tasks;
using CouncilAtelier.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Article ismini sabitle (projedeki olası diğer Article çakışmalarını bitirir)
using ArticleEntity = CouncilAtelier.Models.Article;

namespace CouncilAtelier.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    
    public class AdminMakalelerController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public AdminMakalelerController(CouncilAtelierContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _context.Articles
                .Where(a => !a.IsDeleted)
                .OrderByDescending(a => a.PublishedAt)
                .ToListAsync();

            return View(articles);
        }

        public async Task<IActionResult> Deleted()
        {
            var deletedArticles = await _context.Articles
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted)
                .OrderByDescending(a => a.DeletedAt)
                .ToListAsync();

            return View(deletedArticles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ArticleEntity
            {
                PublishedAt = DateTime.UtcNow
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleEntity model)
        {
            if (string.IsNullOrWhiteSpace(model.Slug))
            {
                ModelState.Remove(nameof(ArticleEntity.Slug));
            }

            model.Slug = string.IsNullOrWhiteSpace(model.Slug)
                ? Slugify(model.Title)
                : Slugify(model.Slug);

            var exists = await _context.Articles
                .IgnoreQueryFilters()
                .AnyAsync(a => a.Slug == model.Slug && a.Id != model.Id);

            if (exists)
            {
                ModelState.AddModelError(nameof(ArticleEntity.Slug),
                    "Bu slug zaten kullanılıyor. Lütfen farklı bir slug gir.");
            }

            if (!ModelState.IsValid)
                return View(model);

            _context.Articles.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private static string Slugify(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim().ToLowerInvariant();

            text = text
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ç", "c");

            text = text.Replace(" ", "-").Replace("_", "-");

            var cleaned = new System.Text.StringBuilder();
            foreach (var ch in text)
            {
                if (char.IsLetterOrDigit(ch) || ch == '-')
                    cleaned.Append(ch);
            }

            var result = cleaned.ToString();
            while (result.Contains("--"))
                result = result.Replace("--", "-");

            return result.Trim('-');
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return NotFound();

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticleEntity model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return NotFound();

            article.Title = model.Title;
            article.Slug = model.Slug;
            article.Summary = model.Summary;
            article.Content = model.Content;
            article.PublishedAt = model.PublishedAt;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return NotFound();

            article.IsDeleted = true;
            article.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var article = await _context.Articles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return NotFound();

            article.IsDeleted = false;
            article.DeletedAt = null;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Deleted));
        }
    }
}