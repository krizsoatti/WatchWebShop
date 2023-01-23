using WatchWebShop.Data;
using WatchWebShop.Data.Base;
using WatchWebShop.Models;

namespace WatchWebShop.Data.Services
{
    public class ManufacturersService : EntityBaseRepository<Manufacturer>, IManufacturersService
    {
        public ManufacturersService(AppDbContext context) : base(context)
        {
        }
    }
}
