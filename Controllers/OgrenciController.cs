using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CouncilAtelier.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CouncilAtelier.Controllers
{
    [Authorize(Roles = "Ogrenci")]
    public class OgrenciController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public OgrenciController(CouncilAtelierContext context)
        {
            _context = context;
        }

        // Öğrenci profil sayfası
        public async Task<IActionResult> Index()
        {
            var ogrenciIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(ogrenciIdStr) || !int.TryParse(ogrenciIdStr, out int ogrenciId))
            {
                return RedirectToAction("Login", "OgrenciAuth");
            }

            var ogrenci = await _context.Ogrenciler
                .Include(o => o.ProgramKayitlari)
                    .ThenInclude(pk => pk.Program)
                        .ThenInclude(p => p.Category)
                .Include(o => o.WorkshopKayitlari)
                    .ThenInclude(wk => wk.Workshop)
                .FirstOrDefaultAsync(o => o.Id == ogrenciId);

            if (ogrenci == null)
            {
                return RedirectToAction("Login", "OgrenciAuth");
            }

            return View(ogrenci);
        }
    }
}
