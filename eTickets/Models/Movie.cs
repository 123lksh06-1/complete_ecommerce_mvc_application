using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public string ImageURL { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set;}
        public MovieCategory MovieCategory { get; set; } // we are creating enum and therefor we will add all enums in data folder

        //relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
        //cinema one to many relationship
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
public Cinema Cinema { get; set; }

        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
    }
}
