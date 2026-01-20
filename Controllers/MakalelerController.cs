using CouncilAtelier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Controllers
{
    public class MakalelerController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public MakalelerController(CouncilAtelierContext context)
        {
            _context = context;
        }

        // /Makaleler
        public async Task<IActionResult> Index()
        {
            var now = DateTime.UtcNow;

            var articles = await _context.Articles
                .Where(a => !a.IsDeleted && a.PublishedAt <= now)
                .OrderByDescending(a => a.PublishedAt)
                .ToListAsync();

            // SEO (liste)
            ViewData["Title"] = "Makaleler";
            ViewData["Description"] = "Council Atelier makaleleri.";

            return View(articles);
        }

        // /Makaleler/{slug}
        [HttpGet("/Makaleler/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return NotFound();

            var now = DateTime.UtcNow;

            var article = await _context.Articles
                .FirstOrDefaultAsync(a =>
                    !a.IsDeleted &&
                    a.PublishedAt <= now &&
                    a.Slug == slug);

            if (article == null)
                return NotFound();

            // SEO (detay)
            ViewData["Title"] = article.Title;

            // Summary varsa onu, yoksa kısa bir içerik parçasını description yap
            var desc = !string.IsNullOrWhiteSpace(article.Summary)
                ? article.Summary
                : (article.Content?.Length > 160 ? article.Content.Substring(0, 160) : article.Content);

            ViewData["Description"] = desc ?? "Council Atelier makalesi.";

            return View(article);
        }

        // /Makaleler/Popup?slug=...
        [HttpGet("/Makaleler/Popup")]
        public async Task<IActionResult> Popup([FromQuery] string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return BadRequest();

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (!isAjax)
                return RedirectToAction(nameof(Details), new { slug });

            var now = DateTime.UtcNow;

            var article = await _context.Articles
                .FirstOrDefaultAsync(a =>
                    !a.IsDeleted &&
                    a.PublishedAt <= now &&
                    a.Slug == slug);

            if (article == null)
                return NotFound();

            return PartialView("~/Views/Makaleler/_ArticlePopup.cshtml", article);
        }
    }
}