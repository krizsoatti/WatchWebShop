using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Data.Services;
using WatchWebShop.Data.Static;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly IOrdersService _ordersService;

        public ProductsController(IProductsService service, IOrdersService ordersService)
        {
            _service = service;
            _ordersService = ordersService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortBy)
        {
            var allProducts = await _service.GetAllAsync(n => n.Manufacturer, c => c.Category);

            //show the most orderes product
            var mostOrdered = await _ordersService.GetAllOrderLines();

            switch (sortBy)
            {
                case "nameDesc":
                    allProducts = allProducts.OrderByDescending(n => n.Name);
                    break;
                case "price":
                    allProducts = allProducts.OrderBy(p => p.UnitPriceNetto);
                    break;
                case "priceDesc":
                    allProducts = allProducts.OrderByDescending(p => p.UnitPriceNetto);
                    break;
                case "mostOrdered": //sum of orderlines quantity for each product
                    allProducts = allProducts.OrderByDescending(m => mostOrdered.Where(p => p.ProductId == m.Id).Sum(q => q.Quantity));
                    break;
                default:
                    allProducts = allProducts.OrderBy(n => n.Name);
                    break;
            }

            return View(allProducts);
        }

        //Get: Products/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownValues();

            ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownValues();

                ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

                return View(product);
            }

            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }


        // GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            if (productDetail == null)
            {
                return View("NotFound");
            }

            return View(productDetail);
        }

        // GET: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            if (productDetail == null)
            {
                return View("NotFound");
            }

            var response = new NewProductVM()
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                UnitPriceNetto = productDetail.UnitPriceNetto,
                ImagePath = productDetail.ImagePath,
                Description = productDetail.Description,
                CategoryId = productDetail.CategoryId,
                ManufacturerId = productDetail.ManufacturerId
            };

            var productDropdownsData = await _service.GetNewProductDropdownValues();

            ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

            return View(productDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownValues();

                ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

                return View(product);
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Products/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            if (productDetail == null)
            {
                return View("NotFound");
            }

            return View(productDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Agb()
        {
            return View("Agb");
        }
    }
}
