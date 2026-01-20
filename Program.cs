using CouncilAtelier.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // MVC
            builder.Services.AddControllersWithViews();

            // DbContext
            builder.Services.AddDbContext<CouncilAtelierContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Auth (Cookie)
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/Denied";
                    options.Cookie.Name = "CouncilAtelier.Auth";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.Use(async (context, next) =>
            {
                // Admin alanında cache kapat (logout sonrası geri tuşu ile içerik görünmesin)
                if (context.Request.Path.StartsWithSegments("/Admin"))
                {
                    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                    context.Response.Headers["Pragma"] = "no-cache";
                    context.Response.Headers["Expires"] = "0";
                }

                await next();
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                await next();

                // Admin sayfaları cachelenmesin (geri tuşunda bile anlık göstermesin)
                if (context.Request.Path.StartsWithSegments("/Admin"))
                {
                    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                    context.Response.Headers["Pragma"] = "no-cache";
                    context.Response.Headers["Expires"] = "0";
                }
            });


            app.Use(async (context, next) =>
            {
                // Admin area response caching kapat
                if (context.Request.Path.StartsWithSegments("/Admin", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
                    context.Response.Headers["Pragma"] = "no-cache";
                    context.Response.Headers["Expires"] = "0";
                }

                await next();
            });


            // 404 -> /not-found
            app.UseStatusCodePagesWithReExecute("/not-found");

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}