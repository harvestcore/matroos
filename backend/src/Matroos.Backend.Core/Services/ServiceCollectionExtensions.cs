using Matroos.Backend.Core.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Matroos.Backend.Core.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services.AddSingleton<IDataContextService, DataContextService>();
    }
}
