using System.Collections.Generic;
using WatchWebShop.Models;

namespace WatchWebShop.Data.ViewModels
{
    public class ManufacturersImagesVM
    {
        public ManufacturersImagesVM()
        {
            Manufacturers = new List<Manufacturer>();
        }

        public List<Manufacturer> Manufacturers { get; set; }

    }
}
