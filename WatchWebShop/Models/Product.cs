namespace WatchWebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPriceNetto { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        public string CategoryId { get; set; }
    }
}
