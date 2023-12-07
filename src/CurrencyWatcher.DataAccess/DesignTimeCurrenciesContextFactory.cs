using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CurrencyWatcher.DataAccess
{
    public class DesignTimeCurrenciesContextFactory : IDesignTimeDbContextFactory<CurrenciesContext>
    {
        public CurrenciesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurrenciesContext>();
            optionsBuilder.UseSqlServer($"Server=localhost\\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;TrustServerCertificate=True;");

            return new CurrenciesContext(optionsBuilder.Options);
        }
    }
}
