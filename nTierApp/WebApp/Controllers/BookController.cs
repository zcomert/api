using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Controllers;

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
    public IActionResult Details([FromRoute(Name ="id")] int id)
    {
        var book = _manager
            .BookService
            .GetBookById(id);
        return View(book);
    }
}
