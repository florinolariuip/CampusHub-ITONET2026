using CampusHub.Domain.Participants;
using Microsoft.EntityFrameworkCore;

namespace CampusHub.Infrastructure.Persistence.Repository;

public class ParticipantRepository(CampusHubDbContext dbContext) : IParticipantRepository
{
    public Task<Participant?> GetParticipantByIdAsync(Guid id)
    {
        return dbContext.Participants.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Participant>> ListParticipantsAsync()
    {
        return await dbContext.Participants.ToListAsync();
    }

    public Task AddParticipantAsync(Participant participant)
    {
        dbContext.Participants.Add(participant);
        return Task.CompletedTask;
    }

    public Task UpdateParticipantAsync(Participant participant)
    {
        dbContext.Participants.Update(participant);
        return Task.CompletedTask;
    }

    public async Task DeleteParticipantAsync(Guid id)
    {
        var participant = await dbContext.Participants.FirstOrDefaultAsync(p => p.Id == id);
        if (participant != null)
        {
            dbContext.Participants.Remove(participant);
        }
    }
}