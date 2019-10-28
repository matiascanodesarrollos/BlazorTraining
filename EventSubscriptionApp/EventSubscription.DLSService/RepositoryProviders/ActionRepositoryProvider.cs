using EventSubscription.DLSService.Context;

namespace EventSubscription.DLSService.RepositoryProviders
{
    public class ActionRepositoryProvider : IActionRepositoryProvider
    {
        public IActionRepository CreateRepository(DbaContext dbContext)
        {
            return new ActionRepository(dbContext);
        }
    }
}
