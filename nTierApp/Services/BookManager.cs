using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contracts;
using System.Linq.Expressions;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILogger<BookManager> _logger;
    public BookManager(IRepositoryManager manager, 
        ILogger<BookManager> logger)
    {
        _manager = manager;
        _logger = logger;
        _logger.LogInformation("BookManager oluşturuldu.");
    }

    public Book CreateBook(Book book)
    {
        if(book is null)
            throw new ArgumentNullException(nameof(book));
        
        _manager.BookRepository.Create(book);
        _manager.SaveChanges();
        _logger.LogInformation($"Yeni kitap oluşturuldu: {book.Title}");
        return book;
    }

    public void DeleteBook(int id, bool trackChanges)
    {
        var entity = GetBookById(id, trackChanges);
        //_bookRepository.Delete(entity);
        _manager.BookRepository.Delete(entity);
        _manager.SaveChanges();
        _logger.LogInformation($"Kitap silindi: {entity.Title}");
    }

    public IEnumerable<Book> GetAllBooks(Expression<Func<Book, bool>> expression = null, 
        bool trackChanges = false)
    {
        _logger.LogInformation("Tüm kitaplar getiriliyor.");
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
        {
            //_logger.LogWarning($"Kitap {bookId} bulunamadı.");
            throw new BookNotFoundException(bookId);
        }
        return book;
    }

    public void UpdateBook(int id, Book book, bool trackChanges)
    {
        var entity = GetBookById(id, trackChanges);
        entity.Title = book.Title;
        entity.Price = book.Price;
        _manager.BookRepository.Update(entity);
        _manager.SaveChanges();
        _logger.LogInformation($"Kitap güncellendi: {entity.Title}");

    }
}
