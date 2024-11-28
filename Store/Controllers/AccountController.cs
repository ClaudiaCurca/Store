using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Authentication;
using Store.Data;
using Store.Models;
using Store.Services;
using System.Data.Entity.Infrastructure;
using System.Security.Claims;

namespace Store.Controllers
{
	public class AccountController : Controller
	{
		public readonly AuthenticationDbContext _context;
		private readonly UserAccounServices accountService;
		private readonly ILogger<HomeController> _logger;

		public AccountController(ILogger<HomeController>logger,AuthenticationDbContext context)
		{
			_logger = logger;
			_context = context;
			accountService = new UserAccounServices(_context);
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Registration() 
		{
			return View();
		}
		[HttpPost]
		public IActionResult Registration(RegistrationViewModel model)
		{
			if(ModelState.IsValid)
			{
				UserAccount account = new UserAccount();
				account.Id = model.Id;
				account.Name = model.Name;
				account.Email = model.Email;
				account.Password = model.Password;
				account.UserName = model.UserName;
				try
				{
					_context.UserAccounts.Add(account);
					_context.SaveChanges();

					ModelState.Clear();
					ViewBag.Message = "User registered successfully";
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError("", "Please enter unique Email or Password");
					return View(model);
					
				}
				return View();
            }
		
			return View(model);
		}
        public IActionResult Login()
        {
            return View();
        }
		[HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
			if (ModelState.IsValid)
			{
				var user = _context.UserAccounts
					.Where(x => (x.UserName == model.UserNameOrEmail || x.Email==model.UserNameOrEmail) && x.Password == model.Password)
					.FirstOrDefault();
				if(user != null)
				{
					//Success, create cookie
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.Email),

					};
					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

					return RedirectToAction("SecurePage");
				}
				else
				{
					ModelState.AddModelError("", "Username/Email or Password is not correct");
				}
			}
            return View(model);
        }
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult SecurePage()
		{
			ViewBag.Name = HttpContext.User.Identity.Name;
			return View();
		}

    }
}
