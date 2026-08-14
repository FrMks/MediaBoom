using MediaBoomService.Infrastructure.Postgres;
using Microsoft.AspNetCore.HttpLogging;

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

        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders
                                  | HttpLoggingFields.ResponsePropertiesAndHeaders
                                  | HttpLoggingFields.Duration;
        });
        services.AddOpenApi();

        return services;
    }
}
