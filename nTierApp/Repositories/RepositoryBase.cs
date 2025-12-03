using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories;

public class RepositoryBase<T> : IRepostioryBase<T>
    where T:class
{
    protected readonly RepositoryContext _context;

    public RepositoryBase(RepositoryContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }


    public T? GetOne(Expression<Func<T, bool>> expression)
    {
        return _context
            .Set<T>()
            .SingleOrDefault(expression);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }
}
