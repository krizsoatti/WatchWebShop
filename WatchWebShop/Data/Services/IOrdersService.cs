using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, Customer customer, Order orders);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
