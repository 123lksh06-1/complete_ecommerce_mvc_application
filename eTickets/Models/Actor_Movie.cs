namespace eTickets.Models
{
    public class Actor_Movie
    { // this table will store actor id and movie id showing many to many relationship
        public int MovieId { get; set; }
        public Movie Movie { get; set; } //actor_movie can have single movie
        public int ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}
