using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Authentication;
using Store.Models;
using Store.Services;
using System.Data.Entity.Infrastructure;
using System.Security.Claims;
using System.Text;
using SHA256Managed = System.Security.Cryptography.SHA256Managed;

namespace Store.Controllers
{
    public class AccountController : Controller
    {
        static byte[] salt = [23, 2, 5, 15, 10];
        public readonly AuthenticationDbContext _context;
        private readonly UserAccounServices accountService;
        private readonly ILogger<HomeController> _logger;

        public AccountController(ILogger<HomeController> logger, AuthenticationDbContext context)
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
            //var salt = GenerateSalt();
            var hashPassword = HashPassword(model.Password, salt);

            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Id = model.Id;
                account.Name = model.Name;
                account.Email = model.Email;
                account.Password = hashPassword;
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
                var user = _context.UserAccounts.FirstOrDefault(x => (x.UserName == model.UserNameOrEmail) || (x.Email == model.UserNameOrEmail));



                if (user != null && verifyPassword(model.Password, user.Password))
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

        private bool verifyPassword(string enteredPassword, string storedPassword)
        {
            var x = HashPassword(enteredPassword, salt);
            if (x != storedPassword) { return false; }

            return true;
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

        static string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                // Concatenate password and salt
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                // Hash the concatenated password and salt
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Concatenate the salt and hashed password for storage
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

                return Convert.ToBase64String(hashedPasswordWithSalt);
            }
        }
    }
}
