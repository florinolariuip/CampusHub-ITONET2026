using CampusHub.Common.CQRS;
using CampusHub.Domain.Common;

namespace CampusHub.Feature.Events.PublishEvent;

public static class PublishEventEndpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/{id:guid}/publish", Handle)
        .WithName("PublishEvent");

    private static async Task<IResult> Handle(
        Guid id,
        ICommandHandler<PublishEventCommand, Unit> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new PublishEventCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Type == ErrorType.NotFound
                ? Results.NotFound(new { result.Error.Code, result.Error.Message })
                : Results.Conflict(new { result.Error.Code, result.Error.Message });
        }

        return Results.NoContent();
    }
}
