using Horsera.Domain.Common;
using Horsera.Domain.Common.Exceptions;
using Horsera.Domain.Entities.HorseAndCare;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Enums;


namespace Horsera.Domain.Entities.Lessons;

public class Lesson : BaseEntity
{
    private readonly List<LessonBooking> _bookings = [];

    private Lesson()
    {
    }

    public Lesson(
        int horseId,
        int instructorId,
        DateTime startAt,
        int durationMinutes,
        int capacity,
        decimal price,
        string lessonType)
    {
        if (horseId <= 0)
        {
            throw new DomainValidationException("A valid horse is required.");
        }

        if (instructorId <= 0)
        {
            throw new DomainValidationException("A valid instructor is required.");
        }

        HorseId = horseId;
        InstructorId = instructorId;
        CreatedAt = DateTime.UtcNow;
        Reschedule(startAt, durationMinutes);
        UpdateCapacityAndPrice(capacity, price);
        LessonType = RequireLessonType(lessonType);
    }

    public DateTime StartAt { get; private set; }
    public int DurationMinutes { get; private set; }

    // Maximum allowed riders for this session.
    public int Capacity { get; private set; }
    public string LessonType { get; private set; } = string.Empty;
    public LessonStatus Status { get; private set; } = LessonStatus.Scheduled;
    public decimal Price { get; private set; }
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public int HorseId { get; private set; }
    public int InstructorId { get; private set; }
    public int? ReceptionistId { get; private set; }

    public Horse Horse { get; private set; } = null!;
    public Instructor Instructor { get; private set; } = null!;
    public Receptionist? Receptionist { get; private set; }


    public IReadOnlyCollection<LessonBooking> Bookings => _bookings;

    public void Reschedule(DateTime startAt, int durationMinutes)
    {
        if (Status != LessonStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled lessons can be rescheduled.");
        }

        if (startAt.Kind != DateTimeKind.Utc)
        {
            throw new DomainValidationException("Lesson start time must be provided in UTC.");
        }

        if (startAt <= DateTime.UtcNow)
        {
            throw new DomainValidationException("A lesson must be scheduled in the future.");
        }

        if (Bookings.Any(booking => IsActiveBooking(booking) && booking.BookedAt >= startAt))
        {
            throw new BusinessRuleException("The new start time conflicts with an existing booking.");
        }

        if (durationMinutes <= 0)
        {
            throw new DomainValidationException("Lesson duration must be greater than zero.");
        }

        StartAt = startAt;
        DurationMinutes = durationMinutes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeAssignment(int horseId, int instructorId)
    {
        EnsureScheduled();

        if (horseId <= 0 || instructorId <= 0)
        {
            throw new DomainValidationException("A valid horse and instructor are required.");
        }

        if (Bookings.Any(IsActiveBooking))
        {
            throw new BusinessRuleException(
                "Horse or instructor cannot be changed while the lesson has active bookings.");
        }

        HorseId = horseId;
        InstructorId = instructorId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCapacityAndPrice(int capacity, decimal price)
    {
        EnsureScheduled();
        if (capacity <= 0)
        {
            throw new DomainValidationException("Lesson capacity must be greater than zero.");
        }

        if (price < 0)
        {
            throw new DomainValidationException("Lesson price cannot be negative.");
        }

        var activeBookings = Bookings.Count(IsActiveBooking);
        if (capacity < activeBookings)
        {
            throw new BusinessRuleException("Capacity cannot be lower than the number of active bookings.");
        }

        Capacity = capacity;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status != LessonStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled lessons can be cancelled.");
        }

        Status = LessonStatus.Cancelled;
        foreach (var booking in Bookings.Where(IsActiveBooking))
        {
            booking.Cancel();
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != LessonStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled lessons can be completed.");
        }

        if (DateTime.UtcNow < StartAt.AddMinutes(DurationMinutes))
        {
            throw new BusinessRuleException("A lesson cannot be completed before its scheduled end.");
        }

        Status = LessonStatus.Completed;
        foreach (var booking in Bookings.Where(booking => booking.Status == BookingStatus.Confirmed))
        {
            booking.Complete();
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public LessonBooking Book(int riderId, DateTime bookedAt)
    {
        if (Status != LessonStatus.Scheduled)
        {
            throw new BusinessRuleException("Bookings can only be added to scheduled lessons.");
        }

        if (bookedAt.Kind != DateTimeKind.Utc)
        {
            throw new DomainValidationException("Booking time must be provided in UTC.");
        }

        if (bookedAt >= StartAt)
        {
            throw new BusinessRuleException("A booking must be made before the lesson starts.");
        }

        if (Bookings.Any(booking =>
                booking.RiderId == riderId && booking.Status != BookingStatus.Cancelled))
        {
            throw new BusinessRuleException("The rider already has an active booking for this lesson.");
        }

        if (Bookings.Count(IsActiveBooking) >= Capacity)
        {
            throw new BusinessRuleException("The lesson has no available capacity.");
        }

        var booking = new LessonBooking(riderId, Price, bookedAt);
        booking.AssignTo(this);
        _bookings.Add(booking);
        return booking;
    }

    private static string RequireLessonType(string lessonType)
    {
        if (string.IsNullOrWhiteSpace(lessonType))
        {
            throw new DomainValidationException("Lesson type is required.");
        }

        return lessonType.Trim();
    }

    private void EnsureScheduled()
    {
        if (Status != LessonStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled lessons can be changed.");
        }
    }

    private static bool IsActiveBooking(LessonBooking booking) =>
        booking.Status is BookingStatus.Pending or BookingStatus.Confirmed;
}
