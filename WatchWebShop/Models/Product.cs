using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchWebShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Netto Price")]
        public double UnitPriceNetto { get; set; }
        
        public string ImagePath { get; set; }

        [Display(Name = "Product Description")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }


    }
}
