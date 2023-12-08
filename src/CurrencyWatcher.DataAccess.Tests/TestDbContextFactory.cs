using Microsoft.EntityFrameworkCore;

namespace CurrencyWatcher.DataAccess.Tests
{
    public class TestDbContextFactory : IDbContextFactory<CurrenciesContext>
    {
        private DbContextOptions<CurrenciesContext> _options;

        public TestDbContextFactory(string databaseName = "InMemoryTest")
        {
            _options = new DbContextOptionsBuilder<CurrenciesContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public CurrenciesContext CreateDbContext()
        {
            return new CurrenciesContext(_options);
        }
    }
}
