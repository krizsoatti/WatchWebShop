using WatchWebShop.Data.Cart;
using WatchWebShop.Models;

namespace WatchWebShop.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
        public double ShoppingCartTotalBrutto { get; set; }
    }
}
