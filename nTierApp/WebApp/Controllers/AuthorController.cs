using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AuthorController : Controller
{
    private readonly IServiceManager _manager;

    public AuthorController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var authors = _manager
            .AuthorService
            .GetAllAuthors();

        return View(authors);
    }
}
