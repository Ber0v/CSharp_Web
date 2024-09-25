
namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Data;
    using CinemaApp.Web.ViewModels.Movie;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;

    public class MovieController : Controller
    {
        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Movie> allMovies = this.dbContext
                .Movies
                .ToList();

            return this.View(allMovies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(AddMovieInputModel inputModel)
        {
            bool isReleaseDateValid = DateTime.TryParseExact(inputModel.ReleasDate, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!isReleaseDateValid)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleasDate), "The Releas Date must be in the following format: dd/MM/yyyy");
                return this.View(inputModel);
            }

            if (this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            Movie movie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                ReleaseDate = releaseDate,
                Director = inputModel.Director,
                Duration = inputModel.Duration,
                Description = inputModel.Description,
            };
            this.dbContext.Movies.Add(movie);
            this.dbContext.SaveChanges();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            bool isIdValid = Guid.TryParse(id, out Guid guidId);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == guidId);

            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }
    }
}
