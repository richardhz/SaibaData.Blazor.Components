using Microsoft.Extensions.DependencyInjection;
using SaibaData.Blazor.Components.Services;

namespace SaibaData.Blazor.Components;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddComponentServices(this IServiceCollection services, Type clientAssemblyIdentifier)
    {
        //the type parameter must be in the top-level namespace

        var clientResourceConfiguration = new ResourceConfiguration(clientAssemblyIdentifier);

        var libraryResourceConfiguration = new ResourceConfiguration(typeof(ServiceCollectionExtensions), "library");
        services.AddSingleton(new AssemblyResourceLocator(libraryResourceConfiguration, clientResourceConfiguration));

        services.AddLocalization();
        services.AddSingleton(_ => new SbrEnvironmentState());

        return services;
    }
}
