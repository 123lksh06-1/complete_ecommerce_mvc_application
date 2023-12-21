using eTickets.Data.Base;
using eTickets.Data.ViewModel;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService :IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);

    }
}
