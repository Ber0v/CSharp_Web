namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.EntityVaIidationConstants.Movie;


    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Fluent Api
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(TitleMaxLength);

            builder.Property(x => x.Genre).IsRequired().HasMaxLength(GenreMaxLength);

            builder.Property(x => x.Director).IsRequired().HasMaxLength(DirectorMaxLength);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(DescriptionMaxLength);

            builder.HasData(this.SeedMovies());
        }
        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "Harry Potter and the Goblet of Fire",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2005, 11, 01),
                    Director = "Mike Newel",
                    Duration = 157,
                    Description = "Description  DescriptionDescriptionDescriptionDescriptionDescription Description Description"
                },
                new Movie()
                {
                    Title = "Lord of the Rings",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2001, 05, 01),
                    Director = "Peter Jacson",
                    Duration = 178,
                    Description = "Description  DescriptionDescriptionDescriptionDescriptionDescription criptionDescriptionDescriptionDescriptionDescriptionDescription Description"
                }
            };

            return movies;
        }
    }
}
