using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders.Include(n => n.OrderLines).ThenInclude(n => n.Product)
                .Where(n => n.Customer.Id == userId).ToListAsync();
            return orders;
        }

        public Task StoreOrderAsync(List<ShoppingCartItem> items, Customer custromer, Order orders, OrderLine orderLines)
        {
            throw new NotImplementedException();
        }
    }
}
