using System.Security.Claims;
using Sidestone.Host.Infrastructure.Interfaces;

namespace Sidestone.Host.Infrastructure.Implementations
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
    {
        public string? UserId =>
            // Retrieve the user ID from the JWT token
            httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Email =>
            // Retrieve the email from the JWT token
            httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        public string? Role =>
            // Retrieve the role from the JWT token
            httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
    }
}
