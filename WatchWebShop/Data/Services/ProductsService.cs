using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WatchWebShop.Data.Base;
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
