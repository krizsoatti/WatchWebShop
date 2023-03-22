using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderInTheDatabaseAsync(List<ShoppingCartItem> items, string customerId, 
            string customerEmail, double totalBrutto, DateTime orderedOn, DateTime paidOn,
            string salutation, string firstName, string lastName, string street, string zipCode,
            string city
            );

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string customerId, string userRole);
        Task<List<Order>> GetAllOrders();

        Task<List<OrderLine>> GetAllOrderLines();

        Task<Order> GetLastOrderAsync(string userId);
        Task<List<OrderLine>> GetLastOrderLineAsync(int orderId);
        Task<List<Product>> GetLastOrderLineProductsAsync(int orderId);
    }
}
