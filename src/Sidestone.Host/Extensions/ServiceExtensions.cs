using System.Reflection;

namespace Sidestone.Host.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// This registers all services, repositories and background services in the specified assemblies.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembliesToScan">These assemblies should contain both the interfaces, and the classes implementing them. The background service assembly should be included as well.</param>
        public static void RegisterServicesAndRepositories(this IServiceCollection services, Assembly[] assembliesToScan)
        {
            services.Scan(scan => scan.FromAssemblies(assembliesToScan).AddClasses().AsMatchingInterface().WithScopedLifetime());
        }
    }
}
