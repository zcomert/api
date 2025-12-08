using Repositories.Contracts;

namespace Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly Lazy<IBookRepository> _bookRepository;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<IAuthorRepository> _authorRepository;

    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
        _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(_context));
    }

    // Bilinçli bir şekilde new kullanıldı.
    public IBookRepository BookRepository =>
        _bookRepository.Value;

    public ICategoryRepository CategoryRepository =>
        _categoryRepository.Value;

    public IAuthorRepository AuthorRepository => 
        _authorRepository.Value;

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
