using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components;

public class TotalCategoriesViewComponent : ViewComponent
{
    private readonly IServiceManager _manager;
    public TotalCategoriesViewComponent(IServiceManager manager)
    {
        _manager = manager;
    }
    public String Invoke()
    {
        return _manager
            .CategoryService
            .GetAllCategories()
            .Count()
            .ToString();
    }
}
