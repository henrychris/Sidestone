using Microsoft.AspNetCore.Identity;
using Sidestone.Host.Data;
using Sidestone.Host.Data.Entities;

namespace Sidestone.Host.Configuration
{
    public static class AuthConfiguration
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.AddAuthentication();
            services.AddAuthorization();
        }
    }
}
