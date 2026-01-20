using CouncilAtelier.Models;
using Microsoft.EntityFrameworkCore;

namespace CouncilAtelier.Data
{
    public class CouncilAtelierContext : DbContext
    {
        public CouncilAtelierContext(DbContextOptions<CouncilAtelierContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Programlar> Programlar { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Workshoplar> Workshoplar { get; set; }
        public DbSet<Basvuru> Basvurular { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<OgrenciProgramKayit> OgrenciProgramKayitlari { get; set; }
        public DbSet<OgrenciWorkshopKayit> OgrenciWorkshopKayitlari { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Global Query Filters (Soft Delete)
            modelBuilder.Entity<Programlar>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Article>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Workshoplar>().HasQueryFilter(w => !w.IsDeleted);

            // Kategoriler
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sinema" },
                new Category { Id = 2, Name = "Fotoğraf" }
            );

            // Programlar / Workshoplar
            modelBuilder.Entity<Programlar>().HasData(
                new Programlar
                {
                    Id = 1,
                    Title = "Sokağın Hafızası: Belgesel ve Sokak Fotoğrafçılığı",
                    Description = "Şehri bir gözlemci gibi okumayı ve karar anını yakalamayı hedefleyen atölye. Sokakta insanla iletişim, görünmez olma teknikleri, ışık-gölge kullanımı ve hikaye bütünlüğü. 1 gün teori + 1 gün saha çekimi.",
                    CategoryId = 2,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Programlar
                {
                    Id = 2,
                    Title = "Bakışın Ritmi: Kompozisyon ve Görsel Estetik",
                    Description = "Teknikten çok gözü eğitmeyi hedefleyen program. Altın oran, denge, negatif alan, renk teorisi ve izleyicinin gözünü kare içinde yönlendirme.",
                    CategoryId = 2,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Programlar
                {
                    Id = 3,
                    Title = "Dijital Karanlık Oda: RAW İşleme ve Stil Geliştirme",
                    Description = "RAW dosyadan kişisel stile uzanan süreç. Lightroom ve Photoshop temelleri, color grading, lokal müdahaleler ve preset oluşturma. Laptop zorunludur.",
                    CategoryId = 2,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Programlar
                {
                    Id = 4,
                    Title = "Kâğıttan Perdeye: Senaryo ve Karakter Tasarımı",
                    Description = "Bir fikrin sinema diline dönüşümü. 3 perde yapısı, karakter arkı, diyalog yazımı ve senaryo formatı (Celtx / Final Draft). 4 haftalık program.",
                    CategoryId = 1,
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Programlar
                {
                    Id = 5,
                    Title = "Kurgu Masasında Hikaye: Montajın Görünmez Gücü",
                    Description = "Kurgu teorisi, ritim, duygu inşası ve görüntü-ses ilişkisi. Kuleshov etkisi ve Adobe Premiere / DaVinci Resolve ile uygulamalar.",
                    CategoryId = 1,
                    IsDeleted = false,
                    DeletedAt = null
                }
            );

            // Makaleler (Slug ile)
            modelBuilder.Entity<Article>().HasData(
                new Article
                {
                    Id = 1,
                    Title = "Fotoğrafik Görüntünün Ontolojisi",
                    Slug = "fotografik-goruntunun-ontolojisi",
                    Summary = "André Bazin, insanın zamanı durdurma arzusunu “Mumya Kompleksi” olarak ele alır; fotoğrafın mekanik süreçle nesnelliği dondurduğunu, sinemanın ise bu nesnelliği zamanın akışına taşıdığını savunur.",
                    Content =
                        "Bazin’e göre plastik sanatların kökeninde insanın ölüme karşı koyma ve zamanı durdurma arzusu vardır (\"Mumya Kompleksi\"). " +
                        "Resim ne kadar gerçekçi olursa olsun sanatçının öznel yorumundan geçer. Fotoğraf ise insan müdahalesi olmadan, ışık ve kimyasal süreçle görüntü üretir; bu yüzden “nesnellik” iddiası taşır. " +
                        "Bu durum fotoğrafın ontolojik statüsünü değiştirir: Fotoğraf bir taklit değil, nesnenin varlığından kopup gelen bir uzantı gibi işler (parmak izi, gölge benzeri). " +
                        "Sinema ise bu gücü ileri taşır: Donmuş görüntüyü zamanın içine yerleştirerek gerçekliğin değişim içindeki nesnelliğini sunar.",
                    PublishedAt = new DateTime(1945, 1, 1),
                    IsDeleted = false,
                    DeletedAt = null
                },
                new Article
                {
                    Id = 2,
                    Title = "Tekniğin Olanaklarıyla Çoğaltılabildiği Çağda Sanat Yapıtı",
                    Slug = "teknigin-olanaklariyla-cogaltildigi-cagda-sanat-yapiti",
                    Summary = "Walter Benjamin, teknik çoğaltımın (fotoğraf/sinema) sanat yapıtının “aura”sını aşındırdığını, sanatın ritüel temelden kopup sergileme ve siyasete eklemlendiğini tartışır.",
                    Content =
                        "Benjamin’e göre modern teknik çoğaltım, sanat yapıtının benzersiz “şimdi ve burada”lığını zayıflatır; bu benzersiz varlık hâline “aura” denir. " +
                        "Çoğaltım, eseri geleneğin alanından çıkarır; nesneye “yakınlaşma” arzusu mesafeye dayalı aurayı çözer. " +
                        "Bu dönüşümle sanat, ritüel/tapınma değerinden sergileme değerine kayar; fotoğraf ve sinema yeni bir algı rejimi üretir. " +
                        "Sinemada oyuncu kameraya oynar; yıldız sistemi yapay bir aura kurar. Kurgu şok etkisiyle algıyı dönüştürür; kamera “optik bilinçdışı”nı görünür kılar. " +
                        "Sonuçta sanat siyasete eklemlenir; Benjamin faşizmin siyaseti estetize etmesine karşı sanatın siyasallaştırılmasını savunur.",
                    PublishedAt = new DateTime(1936, 1, 1),
                    IsDeleted = false,
                    DeletedAt = null
                }
            );
        }
    }
}