using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WatchWebShop.Data.Cart;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Data.ViewComponents
{
    public class ManufacturersLogoSummary : ViewComponent
    {
        private AppDbContext _context { get; set; }
        
        public List<Manufacturer> Manufacturers { get; set; }
        
        public ManufacturersLogoSummary(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var manufacturersLogos = _context.Manufacturers
                .Where(m => m.LogoPath != null)
                .OrderBy(n => n.Name)
                .ToList();

            return View(manufacturersLogos);
        }
    }
}
