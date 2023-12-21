using eTickets.Models;

namespace eTickets.Data.ViewModel
{
    public class NewMovieDropdownVM //since we are using this classto put data from controller to view
    {
        public NewMovieDropdownVM() //new instances of producers, actors, cinemas and we are not defining movie property as it is in Enum, therefore we will use enum to generate movie dropdown
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();


        }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }

    }
}
