using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Our Brands")]
        public string LogoPath { get; set; }

        public List<Product> Products { get; set; }
    }
}
