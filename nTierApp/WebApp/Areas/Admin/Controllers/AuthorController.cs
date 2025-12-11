using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AuthorController : Controller
{
    private readonly IServiceManager _serviceManager;

    public AuthorController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public IActionResult Index()
    {
        var authors = _serviceManager.AuthorService.GetAllAuthors(false);
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.AuthorService.CreateAuthor(author);
            return RedirectToAction("Index");
        }
        return View(author);
    }

    public IActionResult Update(int id)
    {
        var author = _serviceManager.AuthorService.GetAuthorById(id, false);
        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Author author)
    {
        if (ModelState.IsValid)
        {
            _serviceManager.AuthorService.UpdateAuthor(author.AuthorId, author, false);
            return RedirectToAction("Index");
        }
        return View(author);
    }

    public IActionResult Delete(int id)
    {
        var author = _serviceManager.AuthorService.GetAuthorById(id, false);
        return View(author);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _serviceManager.AuthorService.DeleteAuthor(id, false);
        return RedirectToAction("Index");
    }
}
