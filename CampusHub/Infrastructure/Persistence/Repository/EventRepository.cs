using CampusHub.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Infrastructure.Persistence.Repository;

public class EventRepository(CampusHubDbContext dbContext) : IEventRepository
{
    public Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task AddEventAsync(Event @event)
    {
        dbContext.Events.Add(@event);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Event>> GetActiveRangesForParticipantAsync(Guid participantId, DateTime nowUtc,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Events
            .Where(e => dbContext.Registrations.Any(r => r.ParticipantId == participantId && r.EventId == e.Id)
                        && e.DateRange.StartDate <= nowUtc && e.DateRange.EndDate >= nowUtc)
            .ToListAsync(cancellationToken);
    }
}