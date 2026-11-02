using CampusHub.Domain.Common;
using CampusHub.Domain.Registrations;

namespace CampusHub.Domain.Events;

public sealed class Event
{
    
    private readonly List<Registration> _registrations = [];

    private Event()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; } = null!;

    public string? Description { get; private set; }

    public DateRange DateRange { get; private set; } = null!;
    
    public EventType Type { get; private set; }

    public EventStatus Status { get; private set; }
    
    public int Capacity { get; private set; }

    public int Version { get; private set; }
    
    public IReadOnlyCollection<Registration> Registrations => _registrations.AsReadOnly();
    
    public static Result<Event> Create(string title, string? description, DateRange dateRange, EventType type, int capacity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result<Event>.Failure(Error.Validation("InvalidTitle", "Title cannot be empty."));
        }

        if (capacity <= 0)
        {
            return Result<Event>.Failure(Error.Validation("InvalidCapacity", "Capacity must be greater than zero."));
        }

        return Result<Event>.Success(new Event
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            DateRange = dateRange,
            Type = type,
            Status = EventStatus.Draft,
            Capacity = capacity,
            Version = 1
        });
    }
}