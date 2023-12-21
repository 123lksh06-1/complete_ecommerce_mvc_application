using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        private readonly AppDbContext _context;

        public CinemasService(AppDbContext context) : base(context)
        {
        }

    }
}
