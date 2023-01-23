using WatchWebShop.Data.Base;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        public ProductsService(AppDbContext context) : base(context)
        {
        }
    }

}
