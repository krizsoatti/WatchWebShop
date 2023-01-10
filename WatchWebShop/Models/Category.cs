using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category Tax")]
        public decimal TaxRate { get; set; }
    }
}
