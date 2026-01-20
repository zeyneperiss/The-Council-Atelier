using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CouncilAtelier.Data;
using CouncilAtelier.Models;

namespace CouncilAtelier.Controllers
{
    public class WorkshoplarController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public WorkshoplarController(CouncilAtelierContext context)
        {
            _context = context;
        }

        // GET: Workshoplar
        public async Task<IActionResult> Index()
        {
            var workshops = await _context.Workshoplar
                .Include(w => w.Category)
                .Where(w => !w.IsDeleted)
                .OrderByDescending(w => w.EventDate)
                .ToListAsync();

            return View(workshops);
        }

        // GET: Workshoplar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshoplar
                .Include(w => w.Category)
                .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted);

            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }
    }
}
