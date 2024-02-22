using System;
using System.Linq;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T?> GetById(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);

    }
}
