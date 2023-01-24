using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data.Services;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
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

            var filteredProducts = products.Where(n => string.Equals(n.Name, searchTerm, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();
            var filteredManufacturers = manufacturers.Where(n => string.Equals(n.Name, searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();
            var filteredCategories = categories.Where(n => string.Equals(n.Name, searchTerm, StringComparison.CurrentCultureIgnoreCase)).ToList();

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
