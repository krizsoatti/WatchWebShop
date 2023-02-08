using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Salutation")]
        [Required(ErrorMessage = "Salutation is required")]
        public Salutation Salutation { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Display(Name = "Zip code")]
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Display(Name = "User name")]
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password has to contains a combination of uppercase and lowercase letter, number and special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public enum Salutation
    {
        Herr,
        Frau
    }
}
