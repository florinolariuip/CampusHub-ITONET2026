namespace CampusHub.Domain.Events;

public interface IEventRepository
{
    Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddEventAsync(Event @event);       
    
    Task<IReadOnlyList<Event>> GetActiveRangesForParticipantAsync(Guid participantId, DateTime nowUtc, CancellationToken cancellationToken = default);
}