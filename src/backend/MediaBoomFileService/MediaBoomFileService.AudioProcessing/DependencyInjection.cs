using Microsoft.Extensions.DependencyInjection;

namespace MediaBoomFileService.AudioProcessing;

public static class DependencyInjection
{
    public static IServiceCollection AddAudioProcessing(this IServiceCollection services)
    {
        return services;
    }
}
