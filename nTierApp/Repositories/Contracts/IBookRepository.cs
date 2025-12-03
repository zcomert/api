using Entities.Models;

namespace Repositories.Contracts;
public interface IBookRepository
{
    IQueryable<Book> GetAllBooks();
    Book? GetBookById(int bookId);
    void Create(Book book);
    void Update(Book book);
    void Delete(Book book);
}
