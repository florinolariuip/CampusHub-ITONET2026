using CampusHub.Domain.Events;

namespace CampusHub.Feature.Events.GetEventById;

public static class GetEventByIdEndpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/{id:guid}", Handle)
        .WithName("GetEventById");

    private static async Task<IResult> Handle(Guid id, IEventRepository eventRepository,
        CancellationToken cancellationToken)
    {
        var @event = await eventRepository.GetEventByIdAsync(id, cancellationToken);

        if (@event is null)
        {
            return Results.NotFound(new { Error = "Event not found." });
        }

        return Results.Ok(@event);
    }
}