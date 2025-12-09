using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components
{
    public class LastNBookViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public LastNBookViewComponent(IServiceManager manager)
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
}
