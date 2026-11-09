namespace CampusHub.Domain.Registrations;

public interface IRegistrationRepository
{
    Task AddRegistrationAsync(Registration registration, CancellationToken cancellationToken = default);
}
