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

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                UnitPriceNetto = data.UnitPriceNetto,
                ImagePath = data.ImagePath,
                Description = data.Description,
                CategoryId = data.CategoryId,
                ManufacturerId = data.ManufacturerId
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
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

        public async Task UpdateProductAsync(NewProductVM product)
        {
            var dbProduct = _context.Products.FirstOrDefault(n => n.Id == product.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = product.Name;
                dbProduct.UnitPriceNetto = product.UnitPriceNetto;
                dbProduct.ImagePath = product.ImagePath;
                dbProduct.Description = product.Description;
                dbProduct.CategoryId = product.CategoryId;
                dbProduct.ManufacturerId = product.ManufacturerId;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
