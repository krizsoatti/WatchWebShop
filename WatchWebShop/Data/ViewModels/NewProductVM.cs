using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WatchWebShop.Models;

namespace WatchWebShop.Data.ViewModels
{
    public class NewProductVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please enter a product name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Netto Price")]
        [Required(ErrorMessage = "Please enter a netto price")]
        public double UnitPriceNetto { get; set; }

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Please enter a valid image path")]
        public string ImagePath { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Please enter a product description")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Product description must be between 3 and 500 characters")]
        public string Description { get; set; }

        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "Please select a product category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Display(Name = "Product Manufacturer")]
        [Required(ErrorMessage = "Please select a product manufacturer")]
        public int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
    }
}
