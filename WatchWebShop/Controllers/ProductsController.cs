using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Data.Services;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n => n.Manufacturer, c => c.Category);
            return View(allProducts);
        }

        //Get: Products/Create

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Manufacturers = await _service.GetAllManufacturersAsync(),
                Categories = await _service.GetAllCategoriesAsync()
            };
            return response;
        }

        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await GetNewProductDropdownsValues();

            ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("Name, UnitPriceNetto, ImagePath, Description, CategoryId, ManufacturerId")] Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var productDropdownsData = await GetNewProductDropdownsValues();

        //        ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
        //        ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

        //        return View(product);
        //    }
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // GET: Products/Details/1
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
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var productDropdownsData = await GetNewProductDropdownsValues();

        //    ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
        //    ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

        //    return View(product);
        //}

        //public async Task<Product> UpdateAsync(int id, Product newProduct)
        //{
        //    _context.Update(newProduct);
        //    await _context.SaveChangesAsync();
        //    return newProduct;
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, [Bind("Id, Name, UnitPriceNetto, ImagePath, Description, CategoryId, ManufacturerId")] Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return View("NotFound");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        var productDropdownsData = await GetNewProductDropdownsValues();

        //        ViewBag.Manufacturers = new SelectList(productDropdownsData.Manufacturers, "Id", "Name");
        //        ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Name");

        //        return View(product);
        //    }
        //    await UpdateAsync(id, product);
        //    return RedirectToAction(nameof(Index));
        //}

        // GET: Products/Delete/1
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var product = await _context.Products
        //        .Include(m => m.Manufacturer)
        //        .Include(c => c.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return View("NotFound");
        //    }

        //    return View(product);
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return View("NotFound");
        //    }
        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
