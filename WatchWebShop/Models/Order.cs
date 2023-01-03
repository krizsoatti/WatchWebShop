using System;

namespace WatchWebShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPriceBrutto { get; set; }
        public DateTime OrderedOn { get; set; }
        public DateTime PaidOn { get; set; }
        public string RecipientSalutation { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set;}
        public string RecipientStreet { get; set;}
        public string RecipientZipCode { get; set; }
        public string RecipientCity { get; set;}
    }
}
