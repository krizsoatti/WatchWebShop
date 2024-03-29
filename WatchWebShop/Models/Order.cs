﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchWebShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        [ForeignKey("Customer")]
        public string CustomerEmail { get; set; }

        public double TotalPriceBrutto { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime PaidOn { get; set; }

        public string RecipientSalutation { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientStreet { get; set; }
        public string RecipientZipCode { get; set; }
        public string RecipientCity { get; set; }

        public List<OrderLine> OrderLines { get; set; }
    }
}
