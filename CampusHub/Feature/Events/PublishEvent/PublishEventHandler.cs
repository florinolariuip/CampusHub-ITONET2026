using CampusHub.Common.CQRS;
using CampusHub.Domain.Common;
using CampusHub.Domain.Events;

namespace CampusHub.Feature.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand<Unit>;

public sealed class PublishEventHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<PublishEventCommand, Unit>
{
    public async Task<Result<Unit>> HandleAsync(PublishEventCommand command, CancellationToken cancellationToken = default)
    {
        var ev = await eventRepository.GetEventByIdAsync(command.EventId, cancellationToken);
        if (ev is null)
        {
            return Result<Unit>.Failure(Error.NotFound("Event.NotFound", "Event not found."));
        }

        var result = ev.Publish(DateTime.UtcNow);
        if (result.IsFailure)
        {
            return Result<Unit>.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Success(Unit.Value);
    }
}
