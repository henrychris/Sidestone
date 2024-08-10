using System.ComponentModel.DataAnnotations;

namespace Sidestone.Host.Configuration.Settings
{
    public class DatabaseSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; } = null!;
    }
}
