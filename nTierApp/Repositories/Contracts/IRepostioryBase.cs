using System.Linq.Expressions;

namespace Repositories.Contracts;

public interface IRepostioryBase<T>
{     
    IQueryable<T> GetAll(Expression<Func<T, bool>>? expression=null,
        bool trackChanges=false);
    T? GetOne(Expression<Func<T, bool>> expression, 
        bool trackChanges=false);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
