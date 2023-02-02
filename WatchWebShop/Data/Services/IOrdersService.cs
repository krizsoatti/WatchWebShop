using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderInTheDatabaseAsync(List<ShoppingCartItem> items, int customerId, double totalPriceBrutto, string recipientSalutation, string recipientFirstName, string recipientLastName, string recipientStreet, string recipientZipCode, string recipientCity);
        Task<List<Order>> GetOrdersByUserIdAsync(int customerId);
    }
}
