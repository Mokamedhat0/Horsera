using Horsera.Domain.Entities.HorseAndCare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class HorseTreatmentConfiguration : IEntityTypeConfiguration<HorseTreatment>
{
    public void Configure(EntityTypeBuilder<HorseTreatment> builder) =>
        builder.Property(treatment => treatment.Status)
            .HasConversion<string>()
            .HasMaxLength(32);
}
