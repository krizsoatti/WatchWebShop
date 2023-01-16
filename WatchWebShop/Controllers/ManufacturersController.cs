using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data;

namespace WatchWebShop.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly AppDbContext _context;

        public ManufacturersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allManufacturers = await _context.Manufacturers.ToListAsync();
            return View();
        }
    }
}
