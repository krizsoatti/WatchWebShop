using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
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

        public async Task<IActionResult> Index()
        {
            string customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(customerId, userRole);
            return View(orders);
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
            string customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            double totalPriceBrutto = _shoppingCart.GetShoppingCartTotalBrutto();
            DateTime orderedOn = DateTime.Now;
            DateTime paidOn = DateTime.Now;
            //string recipientSalutation = Request.Form["recipientSalutation"];
            //string recipientFirstName = Request.Form["recipientFirstName"];
            //string recipientLastName = Request.Form["recipientLastName"];
            //string recipientStreet = Request.Form["recipientStreet"];
            //string recipientZipCode = Request.Form["recipientZipCode"];
            //string recipientCity = Request.Form["recipientCity"];

 
            await _ordersService.StoreOrderInTheDatabaseAsync(items, customerId, userEmail, totalPriceBrutto, orderedOn, paidOn);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
