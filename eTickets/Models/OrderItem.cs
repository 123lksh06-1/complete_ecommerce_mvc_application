using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class OrderItem //as each order will have multiple order items
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; } //each order item will have amount
        public double Price { get; set; } // price of movie
        public int MovieId { get; set; }// reference to movie and mvc by default will take this as foreign key but you will need to put virtual in front of Movie, but still declaring it
        [ForeignKey("MovieId")]
       // public virtual Movie Movie { get; set; }

        public Movie Movie { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]

        public Order Order { get; set; }



    }
}
