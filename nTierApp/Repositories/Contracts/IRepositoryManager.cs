namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IBookRepository BookRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IAuthorRepository AuthorRepository { get; }
    void SaveChanges();
}
