using eTickets.Data.Services.Interfaces;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(n => n.Cinema);
            return View(data);
        }

        // Get: Movies/Filter
        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync(n => n.Cinema);
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredMovies = data.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredMovies);
            }
            return View("Index", data);
        }

        // Get: Movies/Create
        public async Task<IActionResult> Create()
        {
            var dropdownValues = await _service.GetNewMovieDropDownsValues();
            ViewBag.Cinemas = new SelectList(dropdownValues.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(dropdownValues.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(dropdownValues.Producers, "Id", "FullName");
            return View();
        }

        // Post: Movies/Create
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var dropdownValues = await _service.GetNewMovieDropDownsValues();
                ViewBag.Cinemas = new SelectList(dropdownValues.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(dropdownValues.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(dropdownValues.Producers, "Id", "FullName");
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        // Get: Movies/Details/Id
        public async Task<IActionResult> Details(int Id)
        {
            var MovieDetails = await _service.GetMovieByIdAsync(Id);
            if (MovieDetails == null) return View("NotFound");
            return View(MovieDetails);
        }

        // Get: Movies/Edit/Id
        public async Task<IActionResult> Edit(int Id)
        {
            var MovieDetails = await _service.GetMovieByIdAsync(Id);
            if (MovieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = MovieDetails.Id,
                Name = MovieDetails.Name,
                Description = MovieDetails.Description,
                Price = MovieDetails.Price,
                ImageURL = MovieDetails.ImageURL,
                StartDate = MovieDetails.StartDate,
                EndDate = MovieDetails.EndDate,
                MovieCategory = MovieDetails.MovieCategory,
                CinemaId = MovieDetails.CinemaId,
                ProducerId = MovieDetails.ProducerId,
                ActorIds = MovieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var dropdownValues = await _service.GetNewMovieDropDownsValues();
            ViewBag.Cinemas = new SelectList(dropdownValues.Cinemas, "Id", "Name");
            ViewBag.Actors = new SelectList(dropdownValues.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(dropdownValues.Producers, "Id", "FullName");

            return View(response);
        }

        // Post: Movies/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, NewMovieVM movie)
        {
            if (Id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var dropdownValues = await _service.GetNewMovieDropDownsValues();
                ViewBag.Cinemas = new SelectList(dropdownValues.Cinemas, "Id", "Name");
                ViewBag.Actors = new SelectList(dropdownValues.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(dropdownValues.Producers, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        // Get: Movies/Delete/Id
        public async Task<IActionResult> Delete(int Id)
        {
            var MovieDetails = await _service.GetByIdAsync(Id);
            if (MovieDetails == null) return View("NotFound");
            return View(MovieDetails);
        }

        // Post: Movies/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int Id)
        {
            var MovieDetails = await _service.GetByIdAsync(Id);
            if (MovieDetails == null) return View("NotFound");

            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}