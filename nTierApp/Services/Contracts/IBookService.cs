using Entities.Models;
using System.Linq.Expressions;

namespace Services.Contracts;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks(Expression<Func<Book, bool>> expression=null,
        bool trackChanges=false);
    Book? GetBookById(int bookId, bool trackChanges=false);
    Book CreateBook(Book book);
    void UpdateBook(int id, Book book, bool trackChanges);
    void DeleteBook(int id, bool trackChanges);

}
