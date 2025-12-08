using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contracts;


namespace Services;

public class ServiceManager : IServiceManager
{
  
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IAuthorService> _authorService;

    public ServiceManager(IRepositoryManager repoManager,
        ILoggerFactory loggerFactory)
    {
        _bookService = new Lazy<IBookService>(() => new BookManager(repoManager, 
            loggerFactory.CreateLogger<BookManager>()));
        _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repoManager,
            loggerFactory.CreateLogger<CategoryManager>()));
        _authorService = new Lazy<IAuthorService>(() => new AuthorManager(repoManager,
            loggerFactory.CreateLogger<AuthorManager>()));

    }
    public IBookService BookService => 
        _bookService.Value;

    public ICategoryService CategoryService => 
        _categoryService.Value;

    public IAuthorService AuthorService => 
       _authorService.Value;
}
