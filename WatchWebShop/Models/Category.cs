using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WatchWebShop.Data.Base;

namespace WatchWebShop.Models
{
    public class Category : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double TaxRate { get; set; }

        public List<Product> Products { get; set; }
    }
}
