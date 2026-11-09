using CampusHub.Domain.Common;

namespace CampusHub.Domain.Events;

public sealed record DateRange
{
    private DateRange()
    {
    }
    
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public static Result<DateRange> Create(DateTime startDate, DateTime endDate)
    {
        var startUtc = startDate.ToUniversalTime();
        var endUtc = endDate.ToUniversalTime();
        
        if (startUtc >= endUtc)
        {
            return Result<DateRange>.Failure(Error.Validation("InvalidDateRange", "Start date must be before end date."));
        }
        
        return Result<DateRange>.Success(new DateRange
        {
            StartDate = startUtc,
            EndDate = endUtc
        });
    }
}