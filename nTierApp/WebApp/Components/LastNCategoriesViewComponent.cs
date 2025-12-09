using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components;

public class LastNCategoriesViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;
    public LastNCategoriesViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }
    public IViewComponentResult Invoke(int n)
    {
        var categories = _manager
            .CategoryService
            .GetAllCategories()
            .TakeLast(n);
        
        return View(categories);
    }
}