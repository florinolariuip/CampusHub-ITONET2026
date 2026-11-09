using CampusHub.Domain.Common;

namespace CampusHub.Domain.Events;

public static class EventErrors
{
    public static readonly Error TitleRequired = Error.Validation("Event.TitleRequired", "The event title is required.");
    
    public static readonly Error TitleTooLong = Error.Validation("Event.TitleTooLong", "The event title is too long.");
    
    public static readonly Error DescriptionRequired = Error.Validation("Event.DescriptionRequired", "The event description is required.");
    
    public static readonly Error EventAlreadyPublished = Error.Conflict("Event.AlreadyPublished", "The event has already been published.");

    public static readonly Error EventAlreadyCancelled = Error.Conflict("Event.AlreadyCancelled", "The event has already been cancelled.");
}