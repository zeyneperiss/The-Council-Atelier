using CouncilAtelier.Data;
using CouncilAtelier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Controllers
{
    public class HomeController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public HomeController(CouncilAtelierContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                Programlar = await _context.Programlar
                    .Include(p => p.Category)
                    .Where(p => !p.IsDeleted)
                    .OrderByDescending(p => p.Id)
                    .Take(3)
                    .ToListAsync(),

                Articles = await _context.Articles
                    .OrderByDescending(a => a.PublishedAt)
                    .Take(2)
                    .ToListAsync(),

                Workshoplar = await _context.Workshoplar
                    .Include(w => w.Category)
                    .Where(w => !w.IsDeleted)
                    .OrderByDescending(w => w.EventDate)
                    .Take(4)
                    .ToListAsync()
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/not-found")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound"); // Views/Home/NotFound.cshtml
        }
    }
}