using MediaBoomService.Infrastructure.Postgres;

namespace MediaBoomService.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddWebDependencies(configuration)
            .AddPostgresInfrastructure(configuration);
    }

    private static IServiceCollection AddWebDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IncludeFields = true;
            });

        services.AddHttpLogging();
        services.AddOpenApi();

        return services;
    }
}
