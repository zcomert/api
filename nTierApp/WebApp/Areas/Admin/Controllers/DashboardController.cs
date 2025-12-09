using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IServiceManager _manager;

    public DashboardController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {   
        return View();
    }
}
