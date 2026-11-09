using CampusHub.Domain.Registrations;

namespace CampusHub.Infrastructure.Persistence.Repository;

public class RegistrationRepository(CampusHubDbContext dbContext) : IRegistrationRepository
{
    public Task AddRegistrationAsync(Registration registration, CancellationToken cancellationToken = default)
    {
        dbContext.Registrations.Add(registration);
        return Task.CompletedTask;
    }
}
