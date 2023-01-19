using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WatchWebShop.Data.Base;

namespace WatchWebShop.Models
{
    public class Manufacturer : IEntityBase
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
