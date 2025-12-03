using System.Linq.Expressions;

namespace Repositories.Contracts;

public interface IRepostioryBase<T>
{     IQueryable<T> GetAll();
    T? GetOne(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
