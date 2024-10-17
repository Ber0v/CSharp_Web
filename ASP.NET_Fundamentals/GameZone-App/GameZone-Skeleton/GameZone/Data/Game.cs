using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Data
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string PublisherId { get; set; }

        [Required]
        public IdentityUser Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleasedOn { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<GamerGame> GamersGames { get; set; } = new HashSet<GamerGame>();

        public bool IsDeleted { get; internal set; }
    }
}
