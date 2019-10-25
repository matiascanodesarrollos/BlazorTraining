using EventSubscription.DLSService.Context;
using EventSubscription.Model;

namespace EventSubscription.DLSService
{
    public class ActionRepository : Repository<Action>, IActionRepository
    {
        public ActionRepository(DbaContext dbaContext) : base(dbaContext)
        {
           
        }

    }
}
