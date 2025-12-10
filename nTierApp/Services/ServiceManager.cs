using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contracts;


namespace Services;

public class ServiceManager : IServiceManager
{
  
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<ICategoryService> _categoryService;
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IAuthService> _authService;

    public ServiceManager(IRepositoryManager repoManager,
        ILoggerFactory loggerFactory,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _bookService = new Lazy<IBookService>(() => new BookManager(repoManager, 
            loggerFactory.CreateLogger<BookManager>()));
        _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repoManager,
            loggerFactory.CreateLogger<CategoryManager>()));
        _authorService = new Lazy<IAuthorService>(() => new AuthorManager(repoManager,
            loggerFactory.CreateLogger<AuthorManager>()));
        _authService = new Lazy<IAuthService>(() => new AuthManager(userManager, signInManager, roleManager));
    }
    public IBookService BookService => 
        _bookService.Value;

    public ICategoryService CategoryService => 
        _categoryService.Value;

    public IAuthorService AuthorService => 
       _authorService.Value;

    public IAuthService AuthService => 
       _authService.Value;
}
