using System;
using System.ComponentModel.DataAnnotations;

namespace CouncilAtelier.Models
{
    public class Workshoplar
    {
        public int Id { get; set; }

        [Required, MaxLength(180)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        // Workshop'larda kategori istiyor musun? (Sinema / Fotoğraf gibi)
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Workshop’a özel alanlar (etkinlik mantığı)
        public DateTime? EventDate { get; set; }         // etkinlik tarihi
        [MaxLength(120)]
        public string? Location { get; set; }            // mekan (opsiyonel)
        public int? Capacity { get; set; }               // kontenjan (opsiyonel)

        // Görsel
        [MaxLength(200)]
        public string? ImagePath { get; set; }           // workshop görseli (opsiyonel)

        // Soft delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}