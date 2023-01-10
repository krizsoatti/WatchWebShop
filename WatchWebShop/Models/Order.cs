using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchWebShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public decimal TotalPriceBrutto { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderedOn { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        public DateTime PaidOn { get; set; }

        [Required]
        public string RecipientSalutation { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Recipient First Name")]
        public string RecipientFirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Recipient Last Name")]
        public string RecipientLastName { get; set;}

        [Required]
        [StringLength(50)]
        [Display(Name = "Recipient Address")]
        public string RecipientStreet { get; set;}

        [Required]
        [Display(Name = "Recipient Zip Code")]
        public string RecipientZipCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Recipient City")]
        public string RecipientCity { get; set;}
    }
}
