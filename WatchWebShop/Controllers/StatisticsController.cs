using Microsoft.AspNetCore.Mvc;
using WatchWebShop.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using WatchWebShop.Data.Services;
using System.Threading.Tasks;
using System.Linq;

namespace WatchWebShop.Controllers
{
    public class StatisticsController : Controller
    {
		private readonly IProductsService _service;
		private readonly IOrdersService _ordersService;

        public StatisticsController(IProductsService service, IOrdersService ordersService)
        {
			_service = service;
			_ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
			var mostOrdered = await _ordersService.GetAllOrderLines();
			var allProducts = await _service.GetAllAsync(n => n.Manufacturer, c => c.Category);

			var sumProProduct = allProducts.OrderByDescending(m => mostOrdered.Where(p => p.ProductId == m.Id).Sum(q => q.Quantity));

			List<Charts> dataPoints = new List<Charts>();

			//add product name and sum of quantity to dataPoints
			foreach (var item in sumProProduct)
			{
				dataPoints.Add(new Charts(item.Name, mostOrdered.Where(p => p.ProductId == item.Id).Sum(q => q.Quantity)));
			}

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
        }

		public async Task<IActionResult> IncomeProProduct()
		{
			var orderLines = await _ordersService.GetAllOrderLines();
			var allProducts = await _service.GetAllAsync(n => n.Manufacturer, c => c.Category);

			//add product name and sum of product's price

			//das ist momentan noch falsch
			var sumProProduct = allProducts.OrderByDescending(m => orderLines.Where(p => p.ProductId == m.Id).Sum(q => q.Quantity * q.UnitPriceNetto));
			
			List<Charts> dataPoints = new List<Charts>();
			foreach (var item in sumProProduct)
			{
				dataPoints.Add(new Charts(item.Name, orderLines.Where(p => p.ProductId == item.Id).Sum(q => q.Quantity * q.UnitPriceNetto)));
			}

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
		}
    }
}
