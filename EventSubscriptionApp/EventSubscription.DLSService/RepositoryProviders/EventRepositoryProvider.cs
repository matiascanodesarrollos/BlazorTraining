using EventSubscription.DLSService.Context;

namespace EventSubscription.DLSService.RepositoryProviders
{
    public class EventRepositoryProvider : IEventRepositoryProvider
    {
        public IEventRepository CreateRepository(DbaContext dbContext)
        {
            return new EventRepository(dbContext);
        }
    }
}
