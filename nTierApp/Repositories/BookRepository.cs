using Entities.Models;
using Repositories.Contracts;

namespace Repositories;

public class BookRepository : IBookRepository
{
    private readonly RepositoryContext _context;

    public BookRepository(RepositoryContext context)
    {
        _context = context;
    }

    public void Create(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void Delete(Book book)
    {
        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    public IQueryable<Book> GetAllBooks()
    {
       return _context.Books;
    }

    public Book? GetBookById(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public void Update(Book book)
    {
        throw new NotImplementedException();
    }
}
