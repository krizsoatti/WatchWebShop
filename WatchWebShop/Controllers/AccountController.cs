using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WatchWebShop.Data;
using WatchWebShop.Data.Static;
using WatchWebShop.Data.ViewModels;
using WatchWebShop.Models;

namespace WatchWebShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please try again!";
                return View(loginVM);

            }

            TempData["Error"] = "Wrong credentials. Please try again!";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if(user != null)
            {
                TempData["Error"] = "This email address is already exists!";
                return View(registerVM);
            }

            var newUser = new Customer()
            {
                Email = registerVM.Email,
                EmailConfirmed = true,
                UserName = registerVM.UserName,
                Salutation = registerVM.Salutation,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Street = registerVM.Street,
                ZipCode = registerVM.ZipCode,
                City = registerVM.City,
                PhoneNumber = registerVM.PhoneNumber
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded) 
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }
    }
}
