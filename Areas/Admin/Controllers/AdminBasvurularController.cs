using System.Linq;
using System.Threading.Tasks;
using CouncilAtelier.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBasvurularController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public AdminBasvurularController(CouncilAtelierContext context)
        {
            _context = context;
        }

        // /Admin/AdminBasvurular
        public async Task<IActionResult> Index()
        {
            var list = await _context.Basvurular
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleRead(int id)
        {
            var item = await _context.Basvurular.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            item.IsRead = !item.IsRead;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Basvurular.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();

            _context.Basvurular.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}