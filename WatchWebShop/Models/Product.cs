﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchWebShop.Data.Base;

namespace WatchWebShop.Models
{
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double UnitPriceNetto { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
