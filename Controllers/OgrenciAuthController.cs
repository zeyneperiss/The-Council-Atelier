using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CouncilAtelier.Data;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Controllers
{
    public class OgrenciAuthController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public OgrenciAuthController(CouncilAtelierContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            // Trim ve normalize
            email = (email ?? string.Empty).Trim().ToLowerInvariant();
            password = (password ?? string.Empty);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.Error = "Lütfen e-posta ve şifre giriniz.";
                return View();
            }

            // Veritabanından öğrenciyi bul (email'i lowercase olarak saklandığı için)
            var ogrenci = await _context.Ogrenciler
                .FirstOrDefaultAsync(o => o.Email == email);

            if (ogrenci == null)
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.Error = "E-posta veya şifre hatalı.";
                return View();
            }

            // Şifreyi kontrol et (ŞİFREYİ TRİM ETME - kullanıcı baştaki/sondaki boşlukları şifre olarak kullanabilir)
            var passwordHash = HashPassword(password);
            if (ogrenci.PasswordHash != passwordHash)
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.Error = "E-posta veya şifre hatalı.";
                return View();
            }

            // Giriş başarılı - Claims oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ogrenci.TamAd),
                new Claim(ClaimTypes.Email, ogrenci.Email),
                new Claim(ClaimTypes.NameIdentifier, ogrenci.Id.ToString()),
                new Claim(ClaimTypes.Role, "Ogrenci")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            // Ana sayfaya yönlendir
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
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
