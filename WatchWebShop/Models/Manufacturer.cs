using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
