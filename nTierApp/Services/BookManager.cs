using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System.Linq.Expressions;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    public BookManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public Book CreateBook(Book book)
    {
        if(book is null)
            throw new ArgumentNullException(nameof(book));
        
        _manager.BookRepository.Create(book);
        return book;
    }

    public void DeleteBook(int id, bool trackChanges)
    {
        var entity = GetBookById(id, trackChanges);
        
        if(entity is null)
            throw new ArgumentNullException(nameof(entity));
        
        //_bookRepository.Delete(entity);
        _manager.BookRepository.Delete(entity);
    }

    public IEnumerable<Book> GetAllBooks(Expression<Func<Book, bool>> expression = null, 
        bool trackChanges = false)
    {
        return _manager
            .BookRepository
            .GetAll(expression, trackChanges);
    }

    public Book? GetBookById(int bookId, bool trackChanges = false)
    {
        var book = _manager
            .BookRepository
            .GetOne(b => b.Id.Equals(bookId), trackChanges);

        if (book is null)
            throw new Exception($"Kitap {bookId} bulunamadı!");
        
        return book;
    }

    public void UpdateBook(int id, Book book, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}
