using eTickets.Data.Base;
using eTickets.Data.Enums;
using eTickets.Data.ViewModel;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;//to get data from database
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //add movie actors
            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id, //entity framework will automatically create an id as soon as we add movie to _context
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails= _context.Movies
                .Include(c =>c.Cinema)
                .Include(p =>p.Producer)
                .Include(am=> am.Actors_Movies).ThenInclude(a =>a.Actor)// since am it is  joining entity therfoere include actors but we don't have direct relationship b/w actors and movies
                .FirstOrDefaultAsync(n =>n.Id ==id);
            return await movieDetails;
                }

        public async Task<NewMovieDropdownVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownVM();
            response.Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync();
            response.Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync();
            response.Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync();

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbMovie!=null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }
            //Remove existing actors
            var existingActorsDb =_context.Actors_Movies.Where(n=>n.MovieId==data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();
            

            //add movie actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id, //entity framework will automatically create an id as soon as we add movie to _context
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
    }

