using eTickets.Models;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        //declaring all 5 methods with its return type and parameters as interface is just a contract 
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);//when running app we got error for Cinema.Name in Movies.index cshtml as entity properties weren't passed
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity); //adding data to DB

        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
