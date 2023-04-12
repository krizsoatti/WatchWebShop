using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WatchWebShop.Data.Cart;
using WatchWebShop.Models;
using System.Collections.Generic;

namespace WatchWebShop.Data.ViewModels
{
    public class InvoiceVM
    {
        public double CategoriesTaxRate { get; set; }

        [ForeignKey("OrderLineId")]
        public OrderLine OrderLine { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public int OrderLineId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public double TotalPriceBrutto { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime PaidOn { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Product> Products { get; set; }
        public int Quantity { get; set; }
        public double TotalPriceNetto { get; set; }
        public double TaxRate { get; set; }
    }
}
