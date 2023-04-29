using DDD.Domain.Common.Interfaces;
using DDD.Domain.DomainServices;
using DDD.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace DDD.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Converts domain events to outbox messages
/// </summary>
public sealed class OutboxInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTime;

    public OutboxInterceptor(IDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };

        var events = dbContext.ChangeTracker
            .Entries<IDomainEvents>()
            .Select(e => e.Entity)
            .SelectMany(entity =>
            {
                // Make a copy of the domain events
                var domainEvents = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .Select(evt => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccuredOnUtc = _dateTime.Now,
                Type = evt.GetType().Name,
                // TODO DM: Need to check that this preserves the full type name
                Content = Serialize(evt, settings)
            })
            .ToList();

        await dbContext.Set<OutboxMessage>().AddRangeAsync(events, cancellationToken);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);

    }

    private string Serialize<T>(T entity, JsonSerializerSettings settings)
    {
        var json = JsonConvert.SerializeObject(entity, settings);
        return json;
    }

}

public record Name(string FirstName, string LastName);
