using System.Collections;
using Deopeia.Common.Domain.Auditing;
using Deopeia.Common.Events;

namespace Deopeia.Common.Infrastructure;

public abstract class UnitOfWork<TContext>(
    TContext context,
    IEventProducer eventProducer,
    CurrentUser currentUser
)
    where TContext : DbContext
{
    private readonly TContext _context = context;
    private readonly DbSet<AuditTrail> _auditTrails = context.Set<AuditTrail>();
    private readonly IEventProducer _eventProducer = eventProducer;
    private readonly CurrentUser _currentUser = currentUser;

    public async Task<bool> CommitAsync()
    {
        Audit();
        var eventLogs = LogEvents();
        await _context.SaveChangesAsync();
        await PublishEventsAsync(eventLogs);

        return true;
    }

    private void Audit()
    {
        _context.ChangeTracker.DetectChanges();
        var auditTrails = new List<AuditTrail>();
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            if (
                entry.Entity is AuditTrail
                || entry.State == EntityState.Detached
                || entry.State == EntityState.Unchanged
            )
            {
                continue;
            }

            var keys = new Dictionary<string, object>();
            var oldValues = new Dictionary<string, object?>();
            var newValues = new Dictionary<string, object?>();
            var propertyNames = new List<string>();
            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    keys.Add(propertyName, property.CurrentValue!);
                    continue;
                }

                var oldValue = property.OriginalValue;
                var newValue = property.CurrentValue;
                switch (entry.State)
                {
                    case EntityState.Added:
                        newValues.Add(propertyName, newValue);
                        break;

                    case EntityState.Deleted:
                        oldValues.Add(propertyName, oldValue);
                        break;

                    case EntityState.Modified:
                        if (
                            !property.IsModified
                            || UnitOfWork<TContext>.AreEqual(oldValue, newValue)
                        )
                        {
                            break;
                        }

                        oldValues.Add(propertyName, oldValue);
                        newValues.Add(propertyName, newValue);
                        propertyNames.Add(propertyName);
                        break;
                }
            }

            auditTrails.Add(
                new DataAccessAuditTrail(
                    entry.Entity.GetType(),
                    keys,
                    oldValues,
                    newValues,
                    propertyNames,
                    _currentUser.Id,
                    _currentUser.Address
                )
            );
        }

        _auditTrails.AddRange(auditTrails);
    }

    private static bool AreEqual(object? oldValue, object? newValue)
    {
        if (oldValue is null && newValue is null)
        {
            return true;
        }

        if (oldValue is null || newValue is null)
        {
            return false;
        }

        if (oldValue is ICollection oldItems && newValue is ICollection newItems)
        {
            var oldEnumerator = oldItems.GetEnumerator();
            var newEnumerator = newItems.GetEnumerator();
            while (oldEnumerator.MoveNext())
            {
                if (!newEnumerator.MoveNext())
                {
                    return false;
                }

                if (!UnitOfWork<TContext>.AreEqual(oldEnumerator.Current, newEnumerator.Current))
                {
                    return false;
                }
            }

            return !newEnumerator.MoveNext();
        }

        return oldValue.Equals(newValue);
    }

    private List<EventLog> LogEvents()
    {
        var entities = _context
            .ChangeTracker.Entries()
            .Where(x => x is IHasEvents)
            .Select(x => (IHasEvents)x.Entity)
            .Where(x => x.DomainEvents.Count > 0)
            .ToList();
        var eventLogs = entities
            .SelectMany(x => x.DomainEvents)
            .Select(x => new EventLog(x))
            .ToList();

        _context.Set<EventLog>().AddRange(eventLogs);
        foreach (var entity in entities)
        {
            entity.ClearDomainEvents();
        }

        return eventLogs;
    }

    private async Task PublishEventsAsync(IEnumerable<EventLog> eventLogs)
    {
        var tasks = eventLogs.Select(async eventLog =>
        {
            await _eventProducer.ProduceAsync(eventLog);
        });
        await Task.WhenAll(tasks);
    }
}
