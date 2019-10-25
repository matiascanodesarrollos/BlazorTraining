using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventSubscription.DLSService
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(params object[] keyValues);
        Task<IEnumerable<T>> GetAllAsync();        
        Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> predicate);

        void Add(T element);
        void AddRange(IEnumerable<T> elements);

        void Remove(T element);
        void RemoveRange(IEnumerable<T> elements);

        Task<int> GetCountAsync();        
    }
}
