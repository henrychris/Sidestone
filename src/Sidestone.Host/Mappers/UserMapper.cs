using Sidestone.Host.Data.Entities;
using Sidestone.Host.Features.Auth;
using Sidestone.Host.Features.Auth.SignUp;

namespace Sidestone.Host.Mappers
{
    public static class UserMapper
    {
        internal static ApplicationUser CreateUser(SignUpRequest request)
        {
            return new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.EmailAddress,
                UserName = request.EmailAddress,
                NormalizedUserName = request.EmailAddress.ToUpper(),
            };
        }

        internal static AuthResponse ToAuthResponse(ApplicationUser user, string role, string token)
        {
            return new AuthResponse
            {
                AccessToken = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id
            };
        }
    }
}
