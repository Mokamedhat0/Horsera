using Horsera.Domain.Entities.HorseAndCare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class HorseConfiguration : IEntityTypeConfiguration<Horse>
{
    public void Configure(EntityTypeBuilder<Horse> builder)
    {
        builder.Ignore(horse => horse.Age);
        builder.Property(horse => horse.Status)
            .HasConversion<string>()
            .HasMaxLength(32);
    }
}
