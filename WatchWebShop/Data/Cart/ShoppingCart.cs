using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Cart
{
    public class ShoppingCart
    {
        private AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }   

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.FirstOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.FirstOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = _context.ShoppingCartItems
                       .Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Product)
                       .ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.UnitPriceNetto * c.Amount).Sum();
            return total;
        }
    }
}
