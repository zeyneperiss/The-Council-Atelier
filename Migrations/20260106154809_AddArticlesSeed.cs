using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CouncilAtelier.Migrations
{
    /// <inheritdoc />
    public partial class AddArticlesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Content", "PublishedAt", "Slug", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, "Bazin’e göre plastik sanatların kökeninde insanın ölüme karşı koyma ve zamanı durdurma arzusu vardır (\"Mumya Kompleksi\"). Resim ne kadar gerçekçi olursa olsun sanatçının öznel yorumundan geçer. Fotoğraf ise insan müdahalesi olmadan, ışık ve kimyasal süreçle görüntü üretir; bu yüzden “nesnellik” iddiası taşır. Bu durum fotoğrafın ontolojik statüsünü değiştirir: Fotoğraf bir taklit değil, nesnenin varlığından kopup gelen bir uzantı gibi işler (parmak izi, gölge benzeri). Sinema ise bu gücü ileri taşır: Donmuş görüntüyü zamanın içine yerleştirerek gerçekliğin değişim içindeki nesnelliğini sunar.", new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fotografik-goruntunun-ontolojisi", "André Bazin, insanın zamanı durdurma arzusunu “Mumya Kompleksi” olarak ele alır; fotoğrafın mekanik süreçle nesnelliği dondurduğunu, sinemanın ise bu nesnelliği zamanın akışına taşıdığını savunur.", "Fotoğrafik Görüntünün Ontolojisi" },
                    { 2, "Benjamin’e göre modern teknik çoğaltım, sanat yapıtının benzersiz “şimdi ve burada”lığını zayıflatır; bu benzersiz varlık hâline “aura” denir. Çoğaltım, eseri geleneğin alanından çıkarır; nesneye “yakınlaşma” arzusu mesafeye dayalı aurayı çözer. Bu dönüşümle sanat, ritüel/tapınma değerinden sergileme değerine kayar; fotoğraf ve sinema yeni bir algı rejimi üretir. Sinemada oyuncu kameraya oynar; yıldız sistemi yapay bir aura kurar. Kurgu şok etkisiyle algıyı dönüştürür; kamera “optik bilinçdışı”nı görünür kılar. Sonuçta sanat siyasete eklemlenir; Benjamin faşizmin siyaseti estetize etmesine karşı sanatın siyasallaştırılmasını savunur.", new DateTime(1936, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "teknigin-olanaklariyla-cogaltildigi-cagda-sanat-yapiti", "Walter Benjamin, teknik çoğaltımın (fotoğraf/sinema) sanat yapıtının “aura”sını aşındırdığını, sanatın ritüel temelden kopup sergileme ve siyasete eklemlendiğini tartışır.", "Tekniğin Olanaklarıyla Çoğaltılabildiği Çağda Sanat Yapıtı" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
