using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class GameInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string ReleasedOn { get; set; }

        [Required]
        public string Publisher { get; set; }
    }
}
