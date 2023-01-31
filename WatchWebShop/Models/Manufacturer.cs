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
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Logopath")]
        [Required(ErrorMessage = "LogoPath is required")]
        public string LogoPath { get; set; }

        public List<Product> Products { get; set; }
    }
}
