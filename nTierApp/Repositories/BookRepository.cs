using Entities.Models;
using Repositories.Contracts;

namespace Repositories;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext context) 
        : base(context)
    {

    }
}
