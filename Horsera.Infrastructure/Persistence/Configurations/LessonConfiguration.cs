using Horsera.Domain.Entities.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.Property(lesson => lesson.Status)
            .HasConversion<string>()
            .HasMaxLength(32);

        builder.Navigation(lesson => lesson.Bookings)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
