using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _context.Products.Include(n => n.Manufacturer).Include(c => c.Category).ToListAsync();
            return View(allProducts);
        }

        //Get: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, UnitPriceNetto, ImagePath, Description, CategoryId, ManufacturerId")]Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
