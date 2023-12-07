using CurrencyWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyWatcher.DataAccess
{
    public class CurrenciesContext : DbContext
    {
        public CurrenciesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>().HasKey(p => p.Id);
            modelBuilder.Entity<Currency>().HasIndex(p => p.Code).IsUnique();
            modelBuilder.Entity<ExchangeRate>().HasKey(p => new { p.CurrencyId, p.Date });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
        }
    }
}