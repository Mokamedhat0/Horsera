using Horsera.Domain.Entities.Lessons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class LessonBookingConfiguration : IEntityTypeConfiguration<LessonBooking>
{
    public void Configure(EntityTypeBuilder<LessonBooking> builder)
    {
        builder.Property(booking => booking.Status)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.HasIndex(booking => new { booking.LessonId, booking.RiderId })
            .IsUnique()
            .HasFilter("[Status] <> 'Cancelled'");

        builder.HasOne(booking => booking.Lesson)
            .WithMany(lesson => lesson.Bookings)
            .HasForeignKey(booking => booking.LessonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(booking => booking.Rider)
            .WithMany(rider => rider.LessonBookings)
            .HasForeignKey(booking => booking.RiderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
