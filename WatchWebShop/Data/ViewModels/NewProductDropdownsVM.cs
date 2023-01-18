using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;
using WatchWebShop.Models;

namespace WatchWebShop.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            Manufacturers = new List<Manufacturer>();
            Categories = new List<Category>();
        }

        public List<Manufacturer> Manufacturers { get; set; }
        public List<Category> Categories { get; set; }
    }
}
