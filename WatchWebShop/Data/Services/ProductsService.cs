using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data.Base;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = _context.Categories.ToListAsync();
            return await categories;
        }

        public async Task<List<Manufacturer>> GetAllManufacturersAsync()
        {
            var manufacturers = _context.Manufacturers.ToListAsync();
            return await manufacturers;
        }

        public async Task<ManufacturersImagesVM> GetManufacturersImagesValues()
        {
            var response = new ManufacturersImagesVM();
            response.Manufacturers = await _context.Manufacturers.OrderBy(n => n.LogoPath).ToListAsync();
            return response;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownValues()
        {
            var response = new NewProductDropdownsVM();
            response.Manufacturers = await _context.Manufacturers.OrderBy(n => n.Name).ToListAsync();
            response.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return response;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = _context.Products
                .Include(m => m.Manufacturer)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(n => n.Id == id);
            return await productDetails;
        }
    }

}
