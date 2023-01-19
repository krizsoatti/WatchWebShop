using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Services;

namespace WatchWebShop.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly IManufacturersService _service;

        public ManufacturersController(IManufacturersService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allManufacturers = await _service.GetAllAsync();
            return View(allManufacturers);
        }

        //GET: Manufacturers/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if (manufacturerDetails == null)
            {
                return View("NotFound");
            }
            return View(manufacturerDetails);
        }
    }
}
