using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Database;

namespace MediaBoomService.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MediaBoomServiceDb");
        services.AddScoped<MediaBoomServiceDbContext>(_ =>
            new MediaBoomServiceDbContext(connectionString!));

        services.AddScoped<IDbConnectionFactory, MediaBoomServiceDbContext>(_ =>
            new MediaBoomServiceDbContext(connectionString!));

        return services;
    }
}