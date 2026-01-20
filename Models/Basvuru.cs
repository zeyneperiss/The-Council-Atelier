using System;
using System.ComponentModel.DataAnnotations;

namespace CouncilAtelier.Models
{
    public class Basvuru
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(160)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(30)]
        public string? Phone { get; set; }

        // "Program" / "Workshop" / "Belirsiz"
        [Required, MaxLength(30)]
        public string Type { get; set; } = "Belirsiz";

        // İleride program/workshop adı seçtirmek istersen
        [MaxLength(160)]
        public string? SelectedItem { get; set; }

        [MaxLength(1000)]
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;
    }
}