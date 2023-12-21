using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context) : base(context) { }

        // instead of injecting AppDbContext to ActorsService we will pass AppDbContext to base class which is EntityBaseRepos
    }    
}
