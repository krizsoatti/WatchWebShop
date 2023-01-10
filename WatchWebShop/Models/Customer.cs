using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Customer Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        public string Salutation { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Customer First Name")]        
        public string FistName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Customer Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Customer Address")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Customer Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Customer City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Customer Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
