﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sidestone.Host.Data.Configurations;
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

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
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
