using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWebShop.Data.Base;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Manufacturer>> GetAllManufacturersAsync();
        
        Task<Product> GetProductByIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownValues();
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM product);
        
        Task<ManufacturersImagesVM> GetManufacturersImagesValues();
        
    }
}
