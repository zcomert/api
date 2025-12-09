using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApp.Components
{
    public class TotalBooksViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public TotalBooksViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public String Invoke()
        {
            return _manager
                .BookService
                .GetAllBooks()
                .Count()
                .ToString();
        }
    }
}
