using Horsera.Domain.Entities.HorseAndCare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class HorseMedicalReportConfiguration : IEntityTypeConfiguration<HorseMedicalReport>
{
    public void Configure(EntityTypeBuilder<HorseMedicalReport> builder) =>
        builder.Property(report => report.Status)
            .HasConversion<string>()
            .HasMaxLength(32);
}
