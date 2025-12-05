using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // category/index | /category
        public IActionResult Index()
        {
            return View();
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
