using DocumentFormat.OpenXml.Drawing.Diagrams;
using WatchWebShop.Data.Cart;

namespace WatchWebShop.Data.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
        public double ShoppingCartTotalBrutto { get; set; }
        public Category TaxRate { get; set; }
    }
}
