using CampusHub.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampusHub.Infrastructure.Persistence.Configurations;

public sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.ComplexProperty(e => e.DateRange, dr =>
        {
            dr.Property(d => d.StartDate)
                .IsRequired()
                .HasColumnName("StartDate");

            dr.Property(d => d.EndDate)
                .IsRequired()
                .HasColumnName("EndDate");
        });
    }
}