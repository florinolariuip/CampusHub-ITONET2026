using CampusHub.Common.CQRS;
using CampusHub.Domain.Common;
using CampusHub.Domain.Events;
using CampusHub.Domain.Participants;
using CampusHub.Domain.Registrations;

namespace CampusHub.Feature.Registrations;

public sealed record RegisterForEventCommand(Guid EventId, Guid ParticipantId) : ICommand<Registration>;

public sealed class RegisterForEventHandler(
    IEventRepository eventRepository,
    IParticipantRepository participantRepository,
    IRegistrationRepository registrationRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterForEventCommand, Registration>
{
    public async Task<Result<Registration>> HandleAsync(RegisterForEventCommand command, CancellationToken cancellationToken = default)
    {
        var @event = await eventRepository.GetEventByIdAsync(command.EventId, cancellationToken);
        if (@event is null)
        {
            return Result<Registration>.Failure(Error.NotFound("EventNotFound",
                $"Event with ID {command.EventId} not found."));
        }

        var participant = await participantRepository.GetParticipantByIdAsync(command.ParticipantId);
        if (participant is null)
        {
            return Result<Registration>.Failure(Error.NotFound("ParticipantNotFound",
                $"Participant with ID {command.ParticipantId} not found."));
        }

        var activeRanges = await eventRepository.GetActiveRangesForParticipantAsync(
            command.ParticipantId, DateTime.UtcNow, cancellationToken);

        foreach (var range in activeRanges)
        {
            if (range.DateRange.StartDate < @event.DateRange.EndDate &&
                @event.DateRange.StartDate < range.DateRange.EndDate)
            {
                return Result<Registration>.Failure(Error.Validation("OverlappingEvent",
                    "The participant is already registered for an overlapping event."));
            }
        }

        var registration = Registration.Create(command.EventId, command.ParticipantId);
        await registrationRepository.AddRegistrationAsync(registration, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Registration>.Success(registration);
    }
}
