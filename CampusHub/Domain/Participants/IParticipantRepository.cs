namespace CampusHub.Domain.Participants;

public interface IParticipantRepository
{
    Task<Participant?> GetParticipantByIdAsync(Guid id);
    Task<IEnumerable<Participant>> ListParticipantsAsync();
    Task AddParticipantAsync(Participant participant);
    Task UpdateParticipantAsync(Participant participant);
    Task DeleteParticipantAsync(Guid id);
}