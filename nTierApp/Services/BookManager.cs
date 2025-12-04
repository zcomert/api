using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System.Linq.Expressions;

namespace Services;

public class BookManager : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookManager(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Book CreateBook(Book book)
    {
        if(book is null)
            throw new ArgumentNullException(nameof(book));
        
        _bookRepository.Create(book);
        return book;
    }

    public void DeleteBook(int id, bool trackChanges)
    {
        var entity = GetBookById(id, trackChanges);
        
        if(entity is null)
            throw new ArgumentNullException(nameof(entity));
        
        _bookRepository.Delete(entity);
    }

    public IEnumerable<Book> GetAllBooks(Expression<Func<Book, bool>> expression = null, 
        bool trackChanges = false)
    {
        return _bookRepository.GetAll(expression, trackChanges);
    }

    public Book? GetBookById(int bookId, bool trackChanges = false)
    {
        var book = _bookRepository
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
