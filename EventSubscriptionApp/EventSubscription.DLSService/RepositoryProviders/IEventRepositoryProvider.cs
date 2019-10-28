using EventSubscription.DLSService.Context;

namespace EventSubscription.DLSService.RepositoryProviders
{
    public interface IEventRepositoryProvider
    {
        IEventRepository CreateRepository(DbaContext dbContext);
    }
}