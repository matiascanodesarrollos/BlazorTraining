using Microsoft.EntityFrameworkCore;

namespace EventSubcription.Model.DLS
{
    public class EventContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Events.db");
    }
}
