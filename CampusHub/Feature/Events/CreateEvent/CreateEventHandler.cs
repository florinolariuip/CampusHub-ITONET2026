using CampusHub.Common.CQRS;
using CampusHub.Domain.Common;
using CampusHub.Domain.Events;

namespace CampusHub.Feature.Events.CreateEvent;

public sealed record CreateEventCommand(
    string Title,
    string? Description,
    DateTime StartDate,
    DateTime EndDate,
    EventType Type,
    int Capacity) : ICommand<Event>;

public sealed class CreateEventHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Event>
{
    public async Task<Result<Event>> HandleAsync(CreateEventCommand command, CancellationToken cancellationToken = default)
    {
        var dateRangeResult = DateRange.Create(command.StartDate, command.EndDate);
        if (dateRangeResult.IsFailure)
        {
            return Result<Event>.Failure(dateRangeResult.Error);
        }

        var eventResult = Event.Create(command.Title, command.Description, dateRangeResult.Value, command.Type, command.Capacity);
        if (eventResult.IsFailure)
        {
            return Result<Event>.Failure(eventResult.Error);
        }

        await eventRepository.AddEventAsync(eventResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Event>.Success(eventResult.Value);
    }
}