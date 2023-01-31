using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, Customer custromer, Order orders, OrderLine orderLines);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
