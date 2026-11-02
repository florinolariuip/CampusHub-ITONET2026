using CampusHub.Domain.Common;

namespace CampusHub.Domain.Participants;

public sealed class Participant
{
    private const int MaxNameLenght = 100;

    private Participant()
    {
    }
    
    public Guid Id { get; private set; }

    public string FullName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public static Result<Participant> Create(string fullName, string email)
    {
        
        if(fullName.Length > MaxNameLenght)
        {
            return Result<Participant>.Failure(Error.Validation("FullNameTooLong", "Full name cannot exceed 100 characters."));
        }
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Result<Participant>.Failure(Error.Validation("InvalidFullName", "Full name cannot be empty."));
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            return Result<Participant>.Failure(Error.Validation("InvalidEmail", "Email must be a valid email address."));
        }

        return Result<Participant>.Success(new Participant
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            Email = email
        });
    }
}