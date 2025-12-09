using Microsoft.AspNetCore.Mvc;
using NLog.Config;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class BookController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
