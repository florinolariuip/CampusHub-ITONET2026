namespace CampusHub.Domain.Registrations;

public sealed class Registration
{
    
    private Registration()
    {
    }
    
    public Guid Id { get; private set; }
    
    public Guid EventId { get; private set; }
    
    public Guid ParticipantId { get; private set; }
    
    public RegistrationStatus Status { get; private set; }

    public DateTime RegisteredAtUtc { get; private set; }

    public DateTime? CancelledAtUtc { get; private set; }
    
    internal  static Registration Create(Guid eventId, Guid participantId, RegistrationStatus status = RegistrationStatus.Confirmed, DateTime? registeredAtUtc = null)
    {
        return new Registration
        {
            Id = Guid.NewGuid(),
            EventId = eventId,
            ParticipantId = participantId,
            Status = RegistrationStatus.Confirmed,
            RegisteredAtUtc = DateTime.UtcNow
        };
    }
    
    
    internal void Cancel()
    {
        Status = RegistrationStatus.Cancelled;
        CancelledAtUtc = DateTime.UtcNow;
    }
    
    internal void PromoteToConfirmed()
    {
        Status = RegistrationStatus.Confirmed;
        CancelledAtUtc = null;
    }
    
}