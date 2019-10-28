using EventSubscription.DLSService.RepositoryProviders;
using System.Threading.Tasks;

namespace EventSubscription.DLSService.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbaContext _dbContext;

        public UnitOfWork(DbaContext dbContext, IEventRepositoryProvider eventRepositoryProvider, IActionRepositoryProvider actionRepositoryProvider)
        {
            _dbContext = dbContext;
            Events = eventRepositoryProvider.CreateRepository(dbContext);
            Actions = actionRepositoryProvider.CreateRepository(dbContext);
        }

        public IEventRepository Events { get; private set; }

        public IActionRepository Actions { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
