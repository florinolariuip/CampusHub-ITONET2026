using CampusHub.Common.CQRS;
using CampusHub.Domain.Events;

namespace CampusHub.Feature.Events.CreateEvent;

public static class CreateEventEndpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/", Handle)
        .WithName("CreateEvent");

    private static async Task<IResult> Handle(
        CreateEventCommand command,
        ICommandHandler<CreateEventCommand, Event> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(new { result.Error.Code, result.Error.Message });
        }

        return Results.Created($"/events/{result.Value.Id}", result.Value);
    }
}
