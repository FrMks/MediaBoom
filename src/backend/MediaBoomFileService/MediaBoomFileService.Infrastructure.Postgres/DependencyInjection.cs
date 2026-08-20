using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaBoomFileService.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MediaBoomFileServiceDb")
            ?? throw new InvalidOperationException(
                "Connection string 'MediaBoomFileServiceDb' was not found.");

        services.AddScoped<FileServiceDbContext>(_ =>
            new FileServiceDbContext(connectionString));

        return services;
    }
}
