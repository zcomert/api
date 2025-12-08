using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Expressions;

namespace Repositories;

public class RepositoryBase<T> : IRepositoryBase<T>
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
        //_context.SaveChanges(); // Repository Manager kaydetme işlemini yapacak
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        //_context.SaveChanges();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression=null,
        bool trackChanges = false)
    {
        IQueryable<T> query = _context.Set<T>();
        
        if (expression is not null)
        {
            query = query.Where(expression);
        }

        //if(!trackChanges)
        //{
        //    query = query.AsNoTracking();
        //}
        //return query;

        return trackChanges
            ? query
            : query.AsNoTracking();
    }


    public T? GetOne(Expression<Func<T, bool>> expression, 
        bool trackChanges=false)
    {
        //if(!trackChanges)
        //{
        //    // Değişiklikler izlenmiyor
        //    return _context
        //        .Set<T>()
        //        .AsNoTracking()
        //        .FirstOrDefault(expression);
        //}
        //else
        //{
        //    return _context
        //        .Set<T>()
        //        .SingleOrDefault(expression);
        //}
        return trackChanges 
            ? _context
                .Set<T>()
                .SingleOrDefault(expression)
            : _context
                .Set<T>()
                .AsNoTracking()
                .FirstOrDefault(expression);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        //_context.SaveChanges();
    }
}
