using EventSubscription.Model;
using Microsoft.EntityFrameworkCore;

namespace EventSubscription.DLSService.Context
{
    public class DbaContext : DbContext
    {
        public DbaContext(DbContextOptions<DbaContext> options) : base(options) {  }
        public DbSet<Event> Events { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionKind> ActionKinds { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionOrigin> SubscriptionOrigins { get; set; }
        
    }
}
