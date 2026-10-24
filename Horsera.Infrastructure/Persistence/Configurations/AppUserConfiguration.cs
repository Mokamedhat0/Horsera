using Horsera.Domain.Entities.User;
using Horsera.Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(user => user.UserId);
        builder.HasIndex(user => user.Email)
            .IsUnique();

        builder.HasOne(user => user.Rider)
            .WithOne(rider => rider.User)
            .HasForeignKey<Rider>(rider => rider.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(user => user.Instructor)
            .WithOne(instructor => instructor.User)
            .HasForeignKey<Instructor>(instructor => instructor.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(user => user.Receptionist)
            .WithOne(receptionist => receptionist.User)
            .HasForeignKey<Receptionist>(receptionist => receptionist.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(user => user.StableEmployee)
            .WithOne(employee => employee.User)
            .HasForeignKey<StableEmployee>(employee => employee.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(user => user.Veterinarian)
            .WithOne(veterinarian => veterinarian.User)
            .HasForeignKey<Veterinarian>(veterinarian => veterinarian.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
