using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderInTheDatabaseAsync(List<ShoppingCartItem> items, int customerId);
        Task<List<Order>> GetOrdersByUserIdAsync(int customerId);
    }
}
