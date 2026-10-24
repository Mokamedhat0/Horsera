using Horsera.Domain.Common;
using Horsera.Domain.Common.Exceptions;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Enums;

namespace Horsera.Domain.Entities.Lessons;

public class LessonBooking : BaseEntity
{
    private LessonBooking()
    {
    }

    internal LessonBooking(int riderId, decimal price, DateTime bookedAt)
    {
        if (riderId <= 0)
        {
            throw new DomainValidationException("A valid rider is required.");
        }

        if (price < 0)
        {
            throw new DomainValidationException("Booking price cannot be negative.");
        }

        if (bookedAt.Kind != DateTimeKind.Utc)
        {
            throw new DomainValidationException("Booking time must be provided in UTC.");
        }

        if (bookedAt > DateTime.UtcNow)
        {
            throw new DomainValidationException("Booking time cannot be in the future.");
        }

        RiderId = riderId;
        Price = price;
        BookedAt = bookedAt;
    }

    public int LessonId { get; private set; }
    public int RiderId { get; private set; }

    public DateTime BookedAt { get; private set; }
    public BookingStatus Status { get; private set; } = BookingStatus.Pending;
    public decimal Price { get; private set; }
    public string? PaymentStatus { get; set; }
    public string? Notes { get; set; }

    public Lesson Lesson { get; private set; } = null!;
    public Rider Rider { get; set; } = null!;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    internal void AssignTo(Lesson lesson)
    {
        Lesson = lesson;
        if (lesson.Id > 0)
        {
            LessonId = lesson.Id;
        }
    }

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
        {
            throw new BusinessRuleException("Only pending bookings can be confirmed.");
        }

        if (Lesson.Status != LessonStatus.Scheduled)
        {
            throw new BusinessRuleException("Bookings can only be confirmed for scheduled lessons.");
        }

        Status = BookingStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status is BookingStatus.Cancelled or BookingStatus.Completed)
        {
            throw new BusinessRuleException("This booking can no longer be cancelled.");
        }

        Status = BookingStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != BookingStatus.Confirmed)
        {
            throw new BusinessRuleException("Only confirmed bookings can be completed.");
        }

        if (DateTime.UtcNow < Lesson.StartAt.AddMinutes(Lesson.DurationMinutes))
        {
            throw new BusinessRuleException("A booking cannot be completed before its lesson ends.");
        }

        Status = BookingStatus.Completed;
    }
}
