using EventSubscription.Model;
using Microsoft.EntityFrameworkCore;

namespace EventSubscription.DLSService.Context
{
    internal class DbaContext : DbContext,IDbaContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionKind> ActionKinds { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionOrigin> SubscriptionOrigins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EventSubscription;");
        }
    }
}
