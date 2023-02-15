using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        private readonly UserManager<Customer> _userManager;


        public OrdersController(IProductsService productService, ShoppingCart shoppingCart, IOrdersService ordersService, UserManager<Customer> userManager)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _userManager = userManager;
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

        public async Task<IActionResult> AddItemToShoppingCart2(int id, int piece)
        {
            piece = Convert.ToInt32(HttpContext.Request.Form["quantity"]);

            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                for (int i = 1; i <= piece; i++)
                {
                    _shoppingCart.AddItemToShoppingCart(product);
                }
                
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
            string salutation = User.FindFirstValue("Salutation");
            string firstName = User.FindFirstValue("FirstName");
            string lastName = User.FindFirstValue("LastName");
            string street = User.FindFirstValue("Street");
            string zipCode = User.FindFirstValue("ZipCode");
            string city = User.FindFirstValue("City");

            await _ordersService.StoreOrderInTheDatabaseAsync(items, customerId, userEmail, totalPriceBrutto, orderedOn, paidOn, salutation, firstName, lastName, street, zipCode, city);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }

        public IActionResult ShowInvoice()
        { 
            return View("Rechnung"); 
        }
    }
}
