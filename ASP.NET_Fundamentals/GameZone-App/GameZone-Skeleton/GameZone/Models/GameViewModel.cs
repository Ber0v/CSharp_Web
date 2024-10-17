using GameZone.Data;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class GameViewModel
    {
        [Required]
        [StringLength(10), MinLength(2)]
        public string Title { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(1000), MinLength(10)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ReleasedOn { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

        [Required]
        public int GenreId { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
