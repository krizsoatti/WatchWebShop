using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data.Services;
using WatchWebShop.Data.Static;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SearchController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IManufacturersService _manufacturersService;

        public SearchController(IProductsService productsService, IManufacturersService manufacturersService)
        {
            _productsService = productsService;
            _manufacturersService = manufacturersService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await _productsService.GetAllAsync();
            var manufacturers = await _manufacturersService.GetAllAsync();
            var categories = await _productsService.GetAllCategoriesAsync();

            var filteredProducts = products.Where(n => n.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) || 
            n.Description.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) || 
            n.Manufacturer.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) || 
            n.Category.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();
            var filteredManufacturers = manufacturers.Where(n => n.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();
            var filteredCategories = categories.Where(n => n.Name.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();

            var searchResult = new SearchViewModel
            {
                Products = filteredProducts,
                Manufacturers = filteredManufacturers,
                Categories = filteredCategories
            };
            
            return View("Index", searchResult);
        }

        public class SearchViewModel
        {
            public List<Product> Products { get; set; }
            public List<Manufacturer> Manufacturers { get; set; }
            public List<Category> Categories { get; set; }
        }
}
}
