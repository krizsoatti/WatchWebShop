using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public OrdersService(AppDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string customerId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderLines).ThenInclude(n => n.Product).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.CustomerId == customerId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderInTheDatabaseAsync(List<ShoppingCartItem> items, string customerId, string customerEmail, double totalBrutto, DateTime orderedOn, DateTime paidOn, string salutation, string firstName, string lastName, string street, string zipCode, string city)
        {
            var order = new Order()
            {
                CustomerId = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().Id,
                CustomerEmail = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().Email,
                TotalPriceBrutto = totalBrutto,
                OrderedOn = orderedOn,
                PaidOn = paidOn,
                RecipientSalutation = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().Salutation,
                RecipientFirstName = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().FirstName,
                RecipientLastName = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().LastName,
                RecipientStreet = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().Street,
                RecipientZipCode = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().ZipCode,
                RecipientCity = _userManager.Users.Where(n => n.Id == customerId).FirstOrDefault().City
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderLine()
                {
                    OrderId = order.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    UnitPriceNetto = item.Product.UnitPriceNetto,
                    TaxRate = _context.Categories.Where(n => n.Id == item.Product.CategoryId).FirstOrDefault().TaxRate
                };

                await _context.OrderLines.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetLastOrderAsync(string userId)
        {
            var lastOrder = await _context.Orders.Where(n => n.CustomerId == userId).OrderByDescending(n => n.Id).FirstOrDefaultAsync();
            return lastOrder;
        }

        public async Task<List<OrderLine>> GetLastOrderLineAsync(int orderId)
        {
            var lastOrderLines = await _context.OrderLines.Where(l => l.OrderId == orderId).ToListAsync();
            return lastOrderLines;
        }

        public async Task<List<Product>> GetLastOrderLineProductsAsync(int orderId)
        {
            var orderLinesProducts = await _context.OrderLines.Where(p => p.OrderId == orderId).Include(p => p.Product).ToListAsync();
            var products = from p in orderLinesProducts
                           select p.Product;
            return products.ToList();
        }

        public async Task<List<OrderLine>> GetAllOrderLines()
        {
            var allOrderLines = await _context.OrderLines.ToListAsync();
            return allOrderLines;
        }
    }
}
