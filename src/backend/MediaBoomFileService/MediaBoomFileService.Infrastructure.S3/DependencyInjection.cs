using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediaBoomFileService.Infrastructure.S3;

public static class DependencyInjection
{
    public static IServiceCollection AddS3Infrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
