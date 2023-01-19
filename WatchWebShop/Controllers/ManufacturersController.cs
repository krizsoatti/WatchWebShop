using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Models;
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

        //GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, LogoPath")] Manufacturer manufacturer)
        {
            if(!ModelState.IsValid)
            {
                return View(manufacturer);
            }

            await _service.AddAsync(manufacturer);
            return RedirectToAction(nameof(Index));
        }

        //GET: Manufacturers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var manufacturerDetails = await _service.GetByIdAsync(id);
            if(manufacturerDetails == null)
            {
                return View("NotFound");
            }
            return View(manufacturerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, LogoPath")] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return View(manufacturer);
            }

            if(id == manufacturer.Id)
            {
                await _service.UpdateAsync(id, manufacturer);
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturer);
        }
    }
}
