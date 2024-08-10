namespace Sidestone.Host.Features.Auth
{
    public class AuthResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string AccessToken { get; set; }
        public required string UserId { get; set; }
    }
}
