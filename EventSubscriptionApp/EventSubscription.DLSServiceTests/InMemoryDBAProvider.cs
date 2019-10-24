using EventSubscription.DLSService.Context;
using Microsoft.EntityFrameworkCore;

namespace EventSubscription.DLSServiceTests
{
    static class InMemoryDBAProvider
    {
        public static DbaContext GetDBAContext()
        {
            DbContextOptions<DbaContext> options;
            var builder = new DbContextOptionsBuilder<DbaContext>();
            builder.UseInMemoryDatabase("test");
            options = builder.Options;
            DbaContext dbaContext = new DbaContext(options);
            dbaContext.Database.EnsureDeleted();
            dbaContext.Database.EnsureCreated();

            return dbaContext;
        }
    }
}
