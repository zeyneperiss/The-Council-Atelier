using Microsoft.AspNetCore.Mvc;
using CouncilAtelier.Data;
using CouncilAtelier.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CouncilAtelier.Controllers
{
    public class OgrenciRegisterController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public OgrenciRegisterController(CouncilAtelierContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string ad, string soyad, string email, string telefon, string password, string passwordConfirm)
        {
            // Basit validasyon
            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad) || 
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Lütfen tüm zorunlu alanları doldurun.";
                return View();
            }

            if (password != passwordConfirm)
            {
                ViewBag.Error = "Şifreler eşleşmiyor.";
                return View();
            }

            // Email kontrolü
            email = email.Trim().ToLowerInvariant();
            var emailExists = await _context.Ogrenciler.AnyAsync(o => o.Email == email);
            if (emailExists)
            {
                ViewBag.Error = "Bu e-posta adresi zaten kayıtlı.";
                return View();
            }

            // Şifreyi hashle (basit SHA256)
            var passwordHash = HashPassword(password);

            // Yeni öğrenci oluştur
            var ogrenci = new Ogrenci
            {
                Ad = ad.Trim(),
                Soyad = soyad.Trim(),
                Email = email,
                Telefon = string.IsNullOrWhiteSpace(telefon) ? null : telefon.Trim(),
                PasswordHash = passwordHash,
                KayitTarihi = DateTime.Now
            };

            _context.Ogrenciler.Add(ogrenci);
            await _context.SaveChangesAsync();

            ViewBag.Success = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
            return View("Success");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
