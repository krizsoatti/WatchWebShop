using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Order>> GetOrdersByUserIdAsync(int customerId)
        {
            var orders = await _context.Orders.Include(n => n.OrderLines)
                .ThenInclude(n => n.Product).Where(n => n.CustomerId == customerId).ToListAsync();
            return orders;
        }

        public async Task StoreOrderInTheDatabaseAsync(List<ShoppingCartItem> items, int customerId, double totalPriceBrutto, string recipientSalutation, string recipientFirstName, string recipientLastName, string recipientStreet, string recipientZipCode, string recipientCity)
        {
            var order = new Order()
            {
                CustomerId = customerId,
                TotalPriceBrutto = totalPriceBrutto,
                OrderedOn = DateTime.UtcNow,
                PaidOn = DateTime.UtcNow,
                RecipientSalutation = recipientSalutation,
                RecipientFirstName = recipientFirstName,
                RecipientLastName = recipientLastName,
                RecipientStreet = recipientStreet,
                RecipientZipCode = recipientZipCode,
                RecipientCity = recipientCity,
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
                };

                await _context.OrderLines.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
