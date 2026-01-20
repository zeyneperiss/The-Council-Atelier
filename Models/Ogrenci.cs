using System.ComponentModel.DataAnnotations;

namespace CouncilAtelier.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Ad { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Soyad { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefon { get; set; }

        [Required]
        [MaxLength(500)]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        // İlişkiler - Öğrencinin aldığı programlar/workshoplar
        public ICollection<OgrenciProgramKayit> ProgramKayitlari { get; set; } = new List<OgrenciProgramKayit>();
        public ICollection<OgrenciWorkshopKayit> WorkshopKayitlari { get; set; } = new List<OgrenciWorkshopKayit>();

        // Tam ad property
        public string TamAd => $"{Ad} {Soyad}";
    }

    // Öğrenci - Program ilişki tablosu
    public class OgrenciProgramKayit
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int ProgramId { get; set; }
        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        // Navigation properties
        public Ogrenci Ogrenci { get; set; } = null!;
        public Programlar Program { get; set; } = null!;
    }

    // Öğrenci - Workshop ilişki tablosu
    public class OgrenciWorkshopKayit
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int WorkshopId { get; set; }
        public DateTime KayitTarihi { get; set; } = DateTime.Now;

        // Navigation properties
        public Ogrenci Ogrenci { get; set; } = null!;
        public Workshoplar Workshop { get; set; } = null!;
    }
}
