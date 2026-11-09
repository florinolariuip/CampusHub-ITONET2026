using CampusHub.Domain.Participants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampusHub.Infrastructure.Persistence.Configurations;

public sealed class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("Participants");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(p => p.Email)
            .IsUnique();
    }
}