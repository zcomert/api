using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components;

public class LastNBooksViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;
    public LastNBooksViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }

    public IViewComponentResult Invoke(int n)
    {
        var books = _manager
            .BookService
            .GetAllBooks()
            .TakeLast(n);
        
        return View(books);
    }
}
