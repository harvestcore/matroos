using Matroos.Resources.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Matroos.Resources.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddResourcesServices(this IServiceCollection services)
        {
            return services.AddSingleton<IConfigurationService, ConfigurationService>();
        }
    }
}
