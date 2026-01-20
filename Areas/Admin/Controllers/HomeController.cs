using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CouncilAtelier.Data;

namespace CouncilAtelier.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HomeController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public HomeController(CouncilAtelierContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // İstatistikler
            var stats = new
            {
                TotalArticles = await _context.Articles.CountAsync(a => !a.IsDeleted),
                TotalPrograms = await _context.Programlar.CountAsync(p => !p.IsDeleted),
                TotalWorkshops = await _context.Workshoplar.CountAsync(w => !w.IsDeleted),
                TotalApplications = await _context.Basvurular.CountAsync(),
                UnreadApplications = await _context.Basvurular.CountAsync(b => !b.IsRead),
                TotalStudents = await _context.Ogrenciler.CountAsync(),
                RecentApplications = await _context.Basvurular
                    .OrderByDescending(b => b.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(stats);
        }
    }
}