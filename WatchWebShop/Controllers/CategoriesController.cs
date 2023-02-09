using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Data.Static;

namespace WatchWebShop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCategories = await _context.Categories.ToListAsync();
            return View(allCategories);
        }
    }
}
