using Microsoft.AspNetCore.Mvc;
using NLog.Config;
using Services.Contracts;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class BookController : Controller
{
    private readonly IServiceManager _manager;

    public BookController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        var books = _manager.BookService.GetAllBooks();
        return View(books);
    }
}
