using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WatchWebShop.Models
{
    public class Customer : IdentityUser
    {
        //[DataType(DataType.EmailAddress)]
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
