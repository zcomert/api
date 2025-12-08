using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        // category/index | /category
        public IActionResult Index()
        {
            var categories = _manager
                .CategoryService
                .GetAllCategories(false);

            return View(categories);
        }

        // category/details
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
