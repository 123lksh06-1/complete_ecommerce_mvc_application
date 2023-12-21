using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Data.ViewComponents
{
    
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;//injecting as we need no of items in the shopping cart

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;      
        }
        //to call this viewcomponent we need to have invoke method
        public IViewComponentResult Invoke()
        {
            var items= _shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    }
}
