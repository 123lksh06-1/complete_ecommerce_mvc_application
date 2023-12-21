using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } //a order or multiple orders will be related to user
        public string UserId { get; set; }

        public List<OrderItem> OrderItems { get; set;}

    }
}
