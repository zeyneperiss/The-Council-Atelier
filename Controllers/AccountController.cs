using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CouncilAtelier.Data;

namespace CouncilAtelier.Controllers
{
    public class AccountController : Controller
    {
        private readonly CouncilAtelierContext _context;

        public AccountController(CouncilAtelierContext context)
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
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            username = (username ?? string.Empty).Trim();
            password = (password ?? string.Empty);

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.Error = "Kullanıcı adı/e-posta ve şifre giriniz.";
                return View();
            }

            // 1. Önce admin kontrolü (basit hardcoded)
            if (username == "admin" && password == "Admin123!")
            {
                var adminClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var adminIdentity = new ClaimsIdentity(
                    adminClaims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(adminIdentity)
                );

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "AdminMakaleler", new { area = "Admin" });
            }

            // 2. Öğrenci kontrolü (veritabanından)
            var email = username.ToLowerInvariant();
            var ogrenci = await _context.Ogrenciler
                .FirstOrDefaultAsync(o => o.Email == email);

            if (ogrenci != null)
            {
                var passwordHash = HashPassword(password);
                if (ogrenci.PasswordHash == passwordHash)
                {
                    var ogrenciClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, ogrenci.TamAd),
                        new Claim(ClaimTypes.Email, ogrenci.Email),
                        new Claim(ClaimTypes.NameIdentifier, ogrenci.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Ogrenci")
                    };

                    var ogrenciIdentity = new ClaimsIdentity(
                        ogrenciClaims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(ogrenciIdentity)
                    );

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Ogrenci");
                }
            }

            // Giriş başarısız
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Error = "Kullanıcı adı/e-posta veya şifre hatalı.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Denied()
        {
            // 403 yetkisiz erişim gibi durumlarda burası gösterilecek
            return View();
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