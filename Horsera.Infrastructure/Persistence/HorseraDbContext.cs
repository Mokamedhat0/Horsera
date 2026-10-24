using Horsera.Domain.Entities.HorseAndCare;
using Horsera.Domain.Entities.Lessons;
using Horsera.Domain.Entities.Stables;
using Horsera.Domain.Entities.Staff;
using Horsera.Domain.Entities.User;
using Horsera.Domain.Entities.User.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Horsera.Infrastructure.Persistence;

public sealed class HorseraDbContext(DbContextOptions<HorseraDbContext> options)
    : DbContext(options)
{
    public DbSet<Horse> Horses => Set<Horse>();
    public DbSet<HorseMedicalReport> HorseMedicalReports => Set<HorseMedicalReport>();
    public DbSet<HorseTreatment> HorseTreatments => Set<HorseTreatment>();
    public DbSet<HorseVaccination> HorseVaccinations => Set<HorseVaccination>();
    public DbSet<NutritionPlan> NutritionPlans => Set<NutritionPlan>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonBooking> LessonBookings => Set<LessonBooking>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<StableBuilding> StableBuildings => Set<StableBuilding>();
    public DbSet<StableRoom> StableRooms => Set<StableRoom>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Receptionist> Receptionists => Set<Receptionist>();
    public DbSet<Rider> Riders => Set<Rider>();
    public DbSet<StableEmployee> StableEmployees => Set<StableEmployee>();
    public DbSet<Veterinarian> Veterinarians => Set<Veterinarian>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HorseraDbContext).Assembly);
    }
}
