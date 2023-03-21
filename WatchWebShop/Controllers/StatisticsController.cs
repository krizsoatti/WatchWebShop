using Microsoft.AspNetCore.Mvc;
using WatchWebShop.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WatchWebShop.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            List<Charts> dataPoints = new List<Charts>();

			dataPoints.Add(new Charts("USA", 121));
			dataPoints.Add(new Charts("Great Britain", 67));
			dataPoints.Add(new Charts("China", 70));
			dataPoints.Add(new Charts("Russia", 56));
			dataPoints.Add(new Charts("Germany", 42));
			dataPoints.Add(new Charts("Japan", 41));
			dataPoints.Add(new Charts("France", 42));
			dataPoints.Add(new Charts("South Korea", 21));

			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
        }
    }
}
