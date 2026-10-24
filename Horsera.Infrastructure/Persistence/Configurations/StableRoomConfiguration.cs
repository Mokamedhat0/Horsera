using Horsera.Domain.Entities.Stables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horsera.Infrastructure.Persistence.Configurations;

public sealed class StableRoomConfiguration : IEntityTypeConfiguration<StableRoom>
{
    public void Configure(EntityTypeBuilder<StableRoom> builder) =>
        builder.Navigation(room => room.Horses)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
}
