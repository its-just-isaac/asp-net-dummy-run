using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Monolith.Core.ModuleIntegration
{
    public static class ModularCoreInstaller
    {
        public static IServiceCollection AddModularCore(this IServiceCollection services)
        {
            services.AddControllers().ConfigureApplicationPartManager(manager =>
            {
                // Clear all auto detected controllers.
                manager.ApplicationParts.Clear();

                // Add feature provider to allow "internal" controller
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            // Register a convention allowing to us to prefix routes to modules.
            services.AddTransient<IPostConfigureOptions<MvcOptions>, ModuleRoutingMvcOptionsPostConfigure>();

            return services;
        }
    }
}