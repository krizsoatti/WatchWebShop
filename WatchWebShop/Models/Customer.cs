using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Customer : IdentityUser
    {
        [Key]
        new public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string Salutation { get; set; }     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        override public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
