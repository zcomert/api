namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IBookRepository BookRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    void SaveChanges();
}
