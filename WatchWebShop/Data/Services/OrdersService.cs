using DocumentFormat.OpenXml.Spreadsheet;
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

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string customerId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderLines)
                .ThenInclude(n => n.Product).Where(n => n.CustomerId == customerId).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.CustomerId == customerId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderInTheDatabaseAsync(List<ShoppingCartItem> items, string customerId, string customerEmail, double totalBrutto, DateTime orderedOn, DateTime paidOn /*string salutation, string firstName, string lastName, string street, string zipCode, string city*/)
        {
            var order = new Order()
            {
                CustomerId = customerId,
                CustomerEmail = customerEmail,
                TotalPriceBrutto = totalBrutto,
                OrderedOn = orderedOn,
                PaidOn = paidOn,
                //RecipientSalutation = salutation,
                //RecipientFirstName = firstName,
                //RecipientLastName = lastName,
                //RecipientStreet = street,
                //RecipientZipCode = zipCode,
                //RecipientCity = city
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
