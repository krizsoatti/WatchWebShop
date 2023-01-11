using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchWebShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Netto Price")]
        public decimal UnitPriceNetto { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product ManufacturerID")]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [Required]
        [Display(Name = "Product CategoryID")]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
    }
}
