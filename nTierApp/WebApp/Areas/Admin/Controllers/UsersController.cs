using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
