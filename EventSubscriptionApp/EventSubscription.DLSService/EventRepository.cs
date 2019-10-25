using EventSubscription.Model;
using EventSubscription.DLSService.Context;

namespace EventSubscription.DLSService
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(DbaContext dbaContext) : base(dbaContext)
        {
            
        }

    }
}
