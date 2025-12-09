using Entities.Models;
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromForm] Book book)
    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }
        _manager.BookService.CreateBook(book);
        TempData["Success"] = "Kitap başarılı bir şekilde oluşturuldu.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit([FromRoute(Name ="id")] int id)
    {
        var book = _manager.BookService.GetBookById(id);
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([FromForm] Book book)
    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        _manager.BookService.UpdateBook(book.Id, book, true);
        TempData["Success"] = $"Kitap id: {book.Id} başarılı bir şekilde güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Details([FromRoute(Name = "id")] int id)
    {
        var book = _manager.BookService.GetBookById(id, false);
        return View(book);
    }

    [HttpGet]
    public IActionResult Delete([FromRoute(Name = "id")] int id)
    {
        var book = _manager.BookService.GetBookById(id, false);
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed([FromRoute(Name = "id")] int id)
    {
        _manager.BookService.DeleteBook(id, false);
        TempData["Error"] = "Kitap başarılı bir şekilde silindi.";
        return RedirectToAction(nameof(Index));
    }
}
