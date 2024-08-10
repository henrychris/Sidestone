using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sidestone.Host.Extensions;

namespace Sidestone.Host.Configuration
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Register services in the DI container.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(this IServiceCollection services)
        {
            Assembly[] assemblies = [Assembly.GetExecutingAssembly()];
            services.RegisterServicesAndRepositories(assemblies);

            services.AddSingleton(TimeProvider.System);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
