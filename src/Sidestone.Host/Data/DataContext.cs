using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sidestone.Host.Data.Configurations;
using Sidestone.Host.Data.Converters;
using Sidestone.Host.Data.Entities;

namespace Sidestone.Host.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // we don't want ef core SQL logs.
            optionsBuilder.UseLoggerFactory(
                LoggerFactory.Create(builder =>
                    builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Error)
                )
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddUtcConverterForDateTimeProps(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        private static void AddUtcConverterForDateTimeProps(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var dateTimeProperties = entityType.GetProperties().Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

                foreach (var property in dateTimeProperties)
                {
                    property.SetValueConverter(new DateTimeToUtcConverter());
                }
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken token = default)
        {
            foreach (
                var entity in ChangeTracker
                    .Entries()
                    .Where(x => x.Entity is BaseEntity && x.State == EntityState.Modified)
                    .Select(x => x.Entity)
                    .Cast<BaseEntity>()
            )
            {
                entity.DateUpdated = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
        }
    }
}
