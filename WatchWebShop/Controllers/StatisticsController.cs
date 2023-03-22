using Microsoft.AspNetCore.Mvc;
using WatchWebShop.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using WatchWebShop.Data.Services;
using System.Threading.Tasks;
using System.Linq;
using WatchWebShop.Data;

namespace WatchWebShop.Controllers
{
    public class StatisticsController : Controller
    {
		private readonly IProductsService _service;
		private readonly IOrdersService _ordersService;
		private readonly AppDbContext _context;

        public StatisticsController(IProductsService service, IOrdersService ordersService, AppDbContext context)
        {
			_service = service;
			_ordersService = ordersService;
			_context = context;
        }

        public async Task<IActionResult> Index()
        {
			var allOrderLines = await _ordersService.GetAllOrderLines();
			var allProducts = await _service.GetAllAsync(n => n.Manufacturer, c => c.Category);

			var sumProProduct = allProducts.OrderByDescending(m => allOrderLines.Where(p => p.ProductId == m.Id).Sum(q => q.Quantity));

			List<Charts> dataPoints = new List<Charts>();

			//add product name and sum of quantity to dataPoints
			foreach (var item in sumProProduct)
			{
				dataPoints.Add(new Charts(item.Name, allOrderLines.Where(p => p.ProductId == item.Id).Sum(q => q.Quantity)));
			}

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
        }

		public async Task<IActionResult> GenresChart()
		{
			var allOrderLines = await _ordersService.GetAllOrderLines();
			var allProducts = await _service.GetAllAsync(n => n.Manufacturer, c => c.Category);
			//var allOrders = await _ordersService.GetAllOrders();

			var sumCategoryOrders = allProducts.GroupBy(n => n.Category.Name).Select(n => new { CategoryName = n.Key, Sum = n.Sum(m => m.UnitPriceNetto) });

            List<Charts> dataPoints = new List<Charts>();

            foreach (var item in sumCategoryOrders)
            {
				dataPoints.Add(new Charts(item.CategoryName, item.Sum));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}
