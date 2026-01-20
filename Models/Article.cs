using System;
using System.ComponentModel.DataAnnotations;

namespace CouncilAtelier.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(160)]
        public string Title { get; set; } = string.Empty;

        // URL için: /Makaleler/<slug>
        [Required]
        [MaxLength(200)]
        public string Slug { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Summary { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

    }

    
}