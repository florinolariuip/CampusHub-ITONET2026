using CampusHub.Common.CQRS;
using CampusHub.Domain.Events;

namespace CampusHub.Feature.Events.GetEventById;

public sealed record GetEventBuIdQuery(Guid EventId) : IQuery<Event?>;

public sealed class GetEventByIdQueryHandler(IEventRepository eventRepository)
    : IQueryHandler<GetEventBuIdQuery, Event?>
{
    public async Task<Event?> HandleAsync(GetEventBuIdQuery query, CancellationToken cancellationToken = default)
    {
        var @event = await eventRepository.GetEventByIdAsync(query.EventId, cancellationToken);
        return @event;
    }
}