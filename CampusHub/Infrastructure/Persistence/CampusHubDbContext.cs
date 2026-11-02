using CampusHub.Domain.Common;
using CampusHub.Domain.Events;
using CampusHub.Domain.Participants;
using CampusHub.Domain.Registrations;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Infrastructure.Persistence;

public class CampusHubDbContext(DbContextOptions<CampusHubDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Event> Events => Set<Event>();

    public DbSet<Registration> Registrations => Set<Registration>();

    public DbSet<Participant> Participants => Set<Participant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CampusHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
}