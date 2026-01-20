using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace CouncilAtelier.Migrations
{
    /// <inheritdoc />
    public partial class SeedDemoOgrenci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Demo kullanıcı için şifre hash'i oluştur (Ogrenci123!)
            string passwordHash;
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes("Ogrenci123!");
                var hash = sha256.ComputeHash(bytes);
                passwordHash = Convert.ToBase64String(hash);
            }

            migrationBuilder.InsertData(
                table: "Ogrenciler",
                columns: new[] { "Ad", "Soyad", "Email", "Telefon", "PasswordHash", "KayitTarihi" },
                values: new object[] { "Test", "Öğrenci", "ogrenci@test.com", null, passwordHash, DateTime.Now }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Ogrenciler WHERE Email = 'ogrenci@test.com'");
        }
    }
}
