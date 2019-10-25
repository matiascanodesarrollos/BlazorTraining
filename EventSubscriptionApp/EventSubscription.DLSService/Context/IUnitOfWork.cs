using System;
using System.Threading.Tasks;

namespace EventSubscription.DLSService.Context
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        IActionRepository Actions { get; }

        Task<int> SaveAsync();
    }
}
