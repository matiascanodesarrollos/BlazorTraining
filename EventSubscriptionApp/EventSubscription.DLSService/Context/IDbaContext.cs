using EventSubscription.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSubscription.DLSService.Context
{
    public interface IDbaContext: IDisposable
    {
        DbSet<ActionKind> ActionKinds { get; set; }
        DbSet<EventSubscription.Model.Action> Actions { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<SubscriptionOrigin> SubscriptionOrigins { get; set; }
        DbSet<Subscription> Subscriptions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}