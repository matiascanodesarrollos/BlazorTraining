using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSubscription.DLSService.Context;
using Microsoft.EntityFrameworkCore;

namespace EventSubscription.DLSService
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbaContext _dbContext;

        public Repository(DbaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Remove(T element)
        {
            _dbContext.Set<T>().Remove(element);
        }

        public void RemoveRange(IEnumerable<T> elements)
        {
            _dbContext.Set<T>().RemoveRange(elements);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(params object[] keyValues)
        {
            return await _dbContext.Set<T>().FindAsync(keyValues);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public void Add(T element)
        {
            _dbContext.Set<T>().Add(element);
        }

        public void AddRange(IEnumerable<T> elements)
        {
            _dbContext.Set<T>().AddRange(elements);
        }
    }
}
