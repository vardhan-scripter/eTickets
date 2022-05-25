using eTickets.Data.Base;
using eTickets.Data.Services.Interfaces;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        public readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context) {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM movie)
        {
            var newMovie = new Movie()
            {
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            foreach(var actor in movie.ActorIds)
            {
                var actorMovie = new Actor_Movie()
                {
                    ActorId = actor,
                    MovieId = newMovie.Id
                };

                await _context.Actors_Movies.AddAsync(actorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int Id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies)
                .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == Id);
            return movieDetails;
        }

        public async Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues()
        {
            var response = new NewMovieDropDownsVM()
            {
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM movie)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if (dbMovie != null)
            {
                dbMovie.Name = movie.Name;
                dbMovie.Description = movie.Description;
                dbMovie.Price = movie.Price;
                dbMovie.ImageURL = movie.ImageURL;
                dbMovie.StartDate = movie.StartDate;
                dbMovie.EndDate = movie.EndDate;
                dbMovie.MovieCategory = movie.MovieCategory;
                dbMovie.CinemaId = movie.CinemaId;
                dbMovie.ProducerId = movie.ProducerId;

                await _context.SaveChangesAsync();
            }

            // Remove existing movies
            var existingMovies = await _context.Actors_Movies.Where(n => n.MovieId == movie.Id).ToListAsync();
            _context.Actors_Movies.RemoveRange(existingMovies);
            await _context.SaveChangesAsync();

            foreach (var actor in movie.ActorIds)
            {
                var actorMovie = new Actor_Movie()
                {
                    ActorId = actor,
                    MovieId = movie.Id
                };

                await _context.Actors_Movies.AddAsync(actorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
