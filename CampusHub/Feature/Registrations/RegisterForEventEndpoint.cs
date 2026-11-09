using CampusHub.Common.CQRS;
using CampusHub.Domain.Common;
using CampusHub.Domain.Registrations;

namespace CampusHub.Feature.Registrations;

public static class RegisterForEventEndpoint
{
    public static void Map(RouteGroupBuilder group) => group.MapPost("/", Handle)
        .WithName("RegisterForEvent");

    private static async Task<IResult> Handle(
        RegisterForEventCommand command,
        ICommandHandler<RegisterForEventCommand, Registration> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.Type switch
            {
                ErrorType.NotFound => Results.NotFound(new { result.Error.Code, result.Error.Message }),
                _ => Results.BadRequest(new { result.Error.Code, result.Error.Message })
            };
        }

        return Results.Created($"/registrations/{result.Value.Id}", result.Value);
    }
}
