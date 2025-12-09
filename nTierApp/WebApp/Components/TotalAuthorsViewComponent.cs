using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components;

public class TotalAuthorsViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;
    public TotalAuthorsViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }
    public String Invoke()
    {
        return _manager
            .AuthorService
            .GetAllAuthors()
            .Count()
            .ToString();
    }
}
