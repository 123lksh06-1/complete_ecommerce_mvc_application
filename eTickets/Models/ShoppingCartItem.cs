using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ShoppingCartItem //as before making orders user will select movies into his cart, shoppingcartitems will also be stored in database but after order is completed it will be removed from database
    {
        [Key]
        public int Id { get; set; }
        //each shopping cart item will have a movie and movie amount
        public Movie Movie { get; set; }
        public int Amount { get; set; }
        //each shopping cart item will belong to single shopping cart id
        public string ShoppingCartId { get; set; }//we can create a diffent model for shopping cart then this both will have relationship one to many
    }
}
