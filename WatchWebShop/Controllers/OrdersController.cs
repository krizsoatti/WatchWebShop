using Microsoft.AspNetCore.Mvc;
using WatchWebShop.Data.Cart;
using WatchWebShop.Data.Services;
using WatchWebShop.Data.ViewModels;

namespace WatchWebShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IProductsService productService, ShoppingCart shoppingCart)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
    }
}
