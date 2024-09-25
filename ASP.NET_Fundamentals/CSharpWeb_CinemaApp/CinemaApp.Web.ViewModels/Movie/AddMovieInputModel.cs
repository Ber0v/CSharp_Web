using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels.Movie
{
    using static Common.EntityVaIidationConstants.Movie;
    public class AddMovieInputModel
    {
        [Required]
        [MinLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required]
        public string ReleasDate { get; set; } = null!;

        [Range(DurationMinValue, DurationMaxValue)]
        public int Duration { get; set; }

        [Required]
        [MinLength(DirectorNameMinLength)]
        [MaxLength(DirectorNameMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
    }
}
