using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WatchWebShop.Data.Cart;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private AppDbContext _dbContext;

        public OrdersService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int customerId)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .ThenInclude(p => p.Category)
                .Where(o => o.Id == customerId)
                .ToListAsync();

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, Customer customer, Order orders)
        {
            var order = new Order
            {
                Id = customer.Id,
                TotalPriceBrutto = orders.TotalPriceBrutto,
                OrderedOn = DateTime.Now,
                RecipientSalutation = customer.Salutation,
                RecipientFirstName = customer.FirstName,
                RecipientLastName = customer.LastName,
                RecipientStreet = customer.Street,
                RecipientZipCode = customer.ZipCode,
                RecipientCity = customer.City
            };
            
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            foreach (var item in order.OrderLines)
            {
                var product = _dbContext.Products.Find(item.ProductId);
                var orderLine = new OrderLine
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPriceNetto = product.UnitPriceNetto,
                    TaxRate = product.Category.TaxRate
                };
               await _dbContext.OrderLines.AddAsync(orderLine);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
