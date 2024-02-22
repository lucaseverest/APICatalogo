using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}
