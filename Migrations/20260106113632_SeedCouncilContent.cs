using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CouncilAtelier.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouncilContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Programlar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sinema" },
                    { 2, "Fotoğraf" }
                });

            migrationBuilder.InsertData(
                table: "Programlar",
                columns: new[] { "Id", "CategoryId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Şehri bir gözlemci gibi okumayı ve karar anını yakalamayı hedefleyen atölye. Sokakta insanla iletişim, görünmez olma teknikleri, ışık-gölge kullanımı ve hikaye bütünlüğü. 1 gün teori + 1 gün saha çekimi.", "Sokağın Hafızası: Belgesel ve Sokak Fotoğrafçılığı" },
                    { 2, 2, "Teknikten çok gözü eğitmeyi hedefleyen program. Altın oran, denge, negatif alan, renk teorisi ve izleyicinin gözünü kare içinde yönlendirme.", "Bakışın Ritmi: Kompozisyon ve Görsel Estetik" },
                    { 3, 2, "RAW dosyadan kişisel stile uzanan süreç. Lightroom ve Photoshop temelleri, color grading, lokal müdahaleler ve preset oluşturma. Laptop zorunludur.", "Dijital Karanlık Oda: RAW İşleme ve Stil Geliştirme" },
                    { 4, 1, "Bir fikrin sinema diline dönüşümü. 3 perde yapısı, karakter arkı, diyalog yazımı ve senaryo formatı (Celtx / Final Draft). 4 haftalık program.", "Kâğıttan Perdeye: Senaryo ve Karakter Tasarımı" },
                    { 5, 1, "Kurgu teorisi, ritim, duygu inşası ve görüntü-ses ilişkisi. Kuleshov etkisi ve Adobe Premiere / DaVinci Resolve ile uygulamalar.", "Kurgu Masasında Hikaye: Montajın Görünmez Gücü" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Programlar",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Programlar",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Programlar",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Programlar",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Programlar",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Programlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
