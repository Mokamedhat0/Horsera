using Horsera.Domain.Entities.Lessons;
using Horsera.Domain.Entities.Stables;
using Horsera.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Horsera.Domain.Tests;

public sealed class PersistenceModelTests
{
    [Fact]
    public void Model_MapsAggregateCollectionsAndBookingConstraint()
    {
        var options = new DbContextOptionsBuilder<HorseraDbContext>()
            .UseSqlServer("Server=localhost;Database=Horsera;User Id=test;Password=test;TrustServerCertificate=True")
            .Options;

        using var context = new HorseraDbContext(options);
        var model = context.Model;

        Assert.NotNull(model.FindEntityType(typeof(Lesson))?
            .FindNavigation(nameof(Lesson.Bookings)));
        Assert.NotNull(model.FindEntityType(typeof(StableRoom))?
            .FindNavigation(nameof(StableRoom.Horses)));
        Assert.Contains(
            model.FindEntityType(typeof(LessonBooking))!.GetIndexes(),
            index => index.IsUnique &&
                     index.Properties.Select(property => property.Name)
                         .SequenceEqual([nameof(LessonBooking.LessonId), nameof(LessonBooking.RiderId)]));
    }
}
