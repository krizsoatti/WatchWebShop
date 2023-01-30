using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double TaxRate { get; set; } = 0.20;
    }
}
