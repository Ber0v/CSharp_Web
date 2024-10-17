using System.ComponentModel.DataAnnotations;

namespace GameZone.Data
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
