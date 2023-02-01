using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchWebShop.Models
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public double UnitPriceNetto { get; set; }
        public double TaxRate { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
