using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSubscription.DLSService
{
    public interface IRepository<T>
    {
        Task Insert(T element);
        Task Edit(T element);
        Task Delete(T element);
        Task<IList<T>> GetAll();
        Task<IList<T>> GetAll(int page, int pageSize);
        Task<int> GetCount();        
    }
}
