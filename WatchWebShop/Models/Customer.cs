namespace WatchWebShop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Salutation { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
