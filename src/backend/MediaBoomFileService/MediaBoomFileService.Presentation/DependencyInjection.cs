using MediaBoomFileService.Application;
using MediaBoomFileService.AudioProcessing;
using MediaBoomFileService.Communication;
using MediaBoomFileService.Infrastructure.Postgres;
using MediaBoomFileService.Infrastructure.S3;
using Microsoft.Extensions.DependencyInjection;

namespace MediaBoomFileService.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddApplication()
            .AddCommunication()
            .AddAudioProcessing()
            .AddPostgresInfrastructure(configuration)
            .AddS3Infrastructure(configuration)
            .AddWebDependencies();
    }

    private static IServiceCollection AddWebDependencies(
        this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}
