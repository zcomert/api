using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contracts;


namespace Services;

public class ServiceManager : IServiceManager
{
  
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<ICategoryService> _categoryService;
    
    public ServiceManager(IRepositoryManager repoManager,
        ILoggerFactory loggerFactory)
    {
        _bookService = new Lazy<IBookService>(() => new BookManager(repoManager, 
            loggerFactory.CreateLogger<BookManager>()));
        _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repoManager,
            loggerFactory.CreateLogger<CategoryManager>()));

    }
    public IBookService BookService => 
        _bookService.Value;

    public ICategoryService CategoryService => 
        _categoryService.Value;
}
