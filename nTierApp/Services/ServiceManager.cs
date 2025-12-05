using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
  
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<ICategoryService> _categoryService;
    public ServiceManager(IRepositoryManager repoManager)
    {
        _bookService = new Lazy<IBookService>(() => new BookManager(repoManager));
        _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repoManager));

    }
    public IBookService BookService => 
        _bookService.Value;

    public ICategoryService CategoryService => 
        _categoryService.Value;
}
