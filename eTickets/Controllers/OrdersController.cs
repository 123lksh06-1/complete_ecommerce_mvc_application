using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;//injected shopping cart in orders controller and you need to configure same in startup.cs else will get error InvalidOperationExceptio
        //therefore we are creating session and use it to user can but buy/add items to shopping cart, session is added in shoppingcart class
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService) //inject shopping cart and also movies service
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }
        public IActionResult ShoppingCart()
        {
            var items =_shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item= await _moviesService.GetMovieByIdAsync(id);
            if(item!=null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items= _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";
            //go above and inject orders service for next lines of code
await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();// as soon as order is completed clearing DB
            return View("OrderCompleted");
        }
        public async Task<IActionResult> Index()
        {
            string userId = "";
            var orders = await _ordersService.GetOrdersByUserIdAsync("userId");
            return View(orders);   
        }
    }
}
