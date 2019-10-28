using EventSubscription.DLSService.Context;

namespace EventSubscription.DLSService.RepositoryProviders
{
    public interface IActionRepositoryProvider
    {
        IActionRepository CreateRepository(DbaContext dbContext);
    }
}