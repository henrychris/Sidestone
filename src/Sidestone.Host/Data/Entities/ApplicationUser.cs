using Microsoft.AspNetCore.Identity;

namespace Sidestone.Host.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
