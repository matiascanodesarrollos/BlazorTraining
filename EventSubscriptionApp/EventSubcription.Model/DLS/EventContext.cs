using Microsoft.EntityFrameworkCore;

namespace EventSubcription.Model.DLS
{
    public class EventContext : DbContext
    {
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionKind> ActionKinds { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionOrigin> SubscriptionOrigins { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Events.db");
    }
}
