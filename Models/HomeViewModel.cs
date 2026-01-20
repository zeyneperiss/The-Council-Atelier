using System.Collections.Generic;

namespace CouncilAtelier.Models
{
    public class HomeViewModel
    {
        public List<Programlar> Programlar { get; set; } = new();
        public List<Article> Articles { get; set; } = new();
        public List<Workshoplar> Workshoplar { get; set; } = new();
    }
}