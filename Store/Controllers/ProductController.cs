using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        public readonly StoreContext _context;
        private readonly Services.StoreService storeService;
        private readonly ILogger<HomeController> _logger;
        
        public ProductController(ILogger<HomeController> logger, StoreContext context)
        {
            _logger = logger;
            _context = context;
            storeService = new Services.StoreService(_context);
        }

        public IActionResult Index()
        {
            var allProducts = storeService.GetAll();
            return View(allProducts);
        }
        
        public IActionResult Create()
        {
			return View();
        }
		[HttpPost]
		public IActionResult Create([Bind("Id,Name,Price")] Product product)
		{
			if (!ModelState.IsValid)
			{
				return View(product);
			}
            storeService.Create(product);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
        public List<Product> GetAllProducts()
        {
            var products = storeService.GetAll();
            return products;
        }
        public IActionResult Edit(int id)
        {
            var productDetails = storeService.GetById(id);
            if (productDetails == null)
            {
                return View("NotFound");
            }
            return View(productDetails);
        }

        [HttpPost,ActionName("Edit")]
        public  IActionResult Edit(int id, [Bind("Id,Name,Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            storeService.Update(id, product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var productDetails = storeService.GetById(id);
            if (productDetails == null)
            {
                return View("NotFound");
            }
            return View(productDetails);
        }

        public IActionResult Delete(int id)
        {
            var cinemaDetails = storeService.GetById(id);
            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cinemaDetails = storeService.GetById(id);
            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            storeService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
