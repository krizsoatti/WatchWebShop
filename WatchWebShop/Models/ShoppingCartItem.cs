using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchWebShop.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
