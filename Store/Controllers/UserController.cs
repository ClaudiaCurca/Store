using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
