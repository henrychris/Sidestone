using System.ComponentModel.DataAnnotations;

namespace Sidestone.Host.Configuration.Settings
{
    public class JwtSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string SecretKey { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Issuer { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Audience { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public int TokenLifeTimeInHours { get; set; }
    }
}
