using Horsera.Domain.Common.Exceptions;
using Horsera.Domain.Entities.HorseAndCare;
using Horsera.Domain.Entities.Lessons;
using Horsera.Domain.Entities.Stables;
using Horsera.Domain.Enums;

namespace Horsera.Domain.Tests;

public sealed class LessonTests
{
    [Fact]
    public void Constructor_RejectsInvalidCapacity()
    {
        var startAt = DateTime.UtcNow.AddDays(1);

        Assert.Throws<DomainValidationException>(() =>
            new Lesson(1, 1, startAt, 60, 0, 10m, "Group"));
    }

    [Fact]
    public void Book_RejectsDuplicateAndOverCapacityBookings()
    {
        var lesson = CreateLesson(capacity: 1);
        var bookingTime = DateTime.UtcNow;

        var booking = lesson.Book(1, bookingTime);

        Assert.Throws<BusinessRuleException>(() => lesson.Book(1, bookingTime));
        Assert.Throws<BusinessRuleException>(() => lesson.Book(2, bookingTime));

        booking.Cancel();
        var replacement = lesson.Book(2, bookingTime);

        Assert.Equal(BookingStatus.Pending, replacement.Status);
        Assert.Equal(2, replacement.RiderId);
    }

    [Fact]
    public void Cancel_CancelsActiveBookingsAndPreventsFurtherBookings()
    {
        var lesson = CreateLesson(capacity: 2);
        var booking = lesson.Book(1, DateTime.UtcNow);

        lesson.Cancel();

        Assert.Equal(LessonStatus.Cancelled, lesson.Status);
        Assert.Equal(BookingStatus.Cancelled, booking.Status);
        Assert.Throws<BusinessRuleException>(() => lesson.Book(2, DateTime.UtcNow));
    }

    [Fact]
    public void Complete_RejectsLessonsThatHaveNotEnded()
    {
        var lesson = CreateLesson(capacity: 2);

        Assert.Throws<BusinessRuleException>(lesson.Complete);
    }

    private static Lesson CreateLesson(int capacity) =>
        new(1, 1, DateTime.UtcNow.AddDays(1), 60, capacity, 25m, "Group");
}

public sealed class StableRoomTests
{
    [Fact]
    public void AssignHorse_EnforcesCapacity()
    {
        var room = new StableRoom(1, "Room 1", 1);
        var firstHorse = new Horse("Comet");
        var secondHorse = new Horse("Willow");

        room.AssignHorse(firstHorse);

        Assert.Throws<BusinessRuleException>(() => room.AssignHorse(secondHorse));
        Assert.Same(room, firstHorse.StableRoom);
    }

    [Fact]
    public void AssignHorse_DoesNotAddTheSameHorseTwice()
    {
        var room = new StableRoom(1, "Room 1", 2);
        var horse = new Horse("Comet");

        room.AssignHorse(horse);
        room.AssignHorse(horse);

        Assert.Single(room.Horses);
    }
}

public sealed class HorseTests
{
    [Fact]
    public void Constructor_TrimsRequiredName()
    {
        var horse = new Horse("  Comet  ");

        Assert.Equal("Comet", horse.Name);
    }

    [Fact]
    public void Constructor_RejectsEmptyName()
    {
        Assert.Throws<DomainValidationException>(() => new Horse(" "));
    }
}
