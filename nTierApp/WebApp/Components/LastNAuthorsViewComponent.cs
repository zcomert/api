using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components;

public class LastNAuthorsViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;
    public LastNAuthorsViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }
    public IViewComponentResult Invoke(int n)
    {
        var authors = _manager
            .AuthorService
            .GetAllAuthors()
            .TakeLast(n);
        
        return View(authors);
    }
}