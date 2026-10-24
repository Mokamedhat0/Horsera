using Horsera.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Horsera.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HorseraDatabase");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "The 'HorseraDatabase' connection string is required. " +
                "Configure ConnectionStrings:HorseraDatabase or the " +
                "ConnectionStrings__HorseraDatabase environment variable.");
        }

        services.AddDbContext<HorseraDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
