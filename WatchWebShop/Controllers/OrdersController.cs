using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WatchWebShop.Data.Cart;
using WatchWebShop.Data.Services;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;


        public OrdersController(IProductsService productService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                ShoppingCartTotalBrutto = _shoppingCart.GetShoppingCartTotalBrutto(),
                CategoriesTaxRate = _shoppingCart.GetCategoriesTaxRate()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.AddItemToShoppingCart(product);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.RemoveItemFromCart(product);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            int customerId = 1;
            double totalPriceBrutto = _shoppingCart.GetShoppingCartTotalBrutto();
            string recipientSalutation = "Mr.";
            string recipientFirstName = "John";
            string recipientLastName = "Doe";
            string recipientStreet = "Main Street 1";
            string recipientZipCode = "12345";
            string recipientCity = "New York";

            await _ordersService.StoreOrderInTheDatabaseAsync(items, customerId, totalPriceBrutto, recipientSalutation, recipientFirstName, recipientLastName, recipientStreet, recipientZipCode, recipientCity);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
