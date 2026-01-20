using CouncilAtelier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Controllers
{
    public class ProgramlarController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public ProgramlarController(CouncilAtelierContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var programlar = await _context.Programlar
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return View(programlar);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var program = await _context.Programlar
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (program == null)
                return NotFound();

            return View(program);
        }

    }
}