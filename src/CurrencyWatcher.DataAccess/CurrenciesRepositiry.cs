using CurrencyWatcher.Domain.Infrastructure;
using CurrencyWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyWatcher.DataAccess
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private readonly IDbContextFactory<CurrenciesContext> _contextFactory;

        public CurrenciesRepository(IDbContextFactory<CurrenciesContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task SaveCurrenciesAsync(Currency[] currencies)
        {
            using var context = _contextFactory.CreateDbContext();
            var existingCurrencies = await context.ExchangeRates
                .GroupBy(x => new { x.Currency.Id, x.Currency.Code })
                .Select(x =>
                    new
                    {
                        x.Key.Id,
                        x.Key.Code,
                        MaxDate = x.Max(c => c.Date)
                    })
                .ToListAsync();
            foreach (var currency in currencies)
            {
                var existingCurrency = existingCurrencies.FirstOrDefault(x => x.Code == currency.Code);
                if (existingCurrency != null)
                {

                    context.ExchangeRates
                        .AddRange(
                            currency
                                .Rates
                                .Where(x => x.Date > existingCurrency.MaxDate)
                                .Select(x => new ExchangeRate
                                {
                                    CurrencyId = existingCurrency.Id,
                                    Date = x.Date,
                                    Rate = x.Rate
                                }));

                }
                else
                {
                    context.Currencies.Add(currency);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task<CurrencyDTO[]> GetAllCurrenciesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Currencies.Select(x => new CurrencyDTO { Id = x.Id, CurrencyCode = x.Code }).ToArrayAsync();
        }

        public async Task<CurrencyRateDTO?> GetCurrencyRateAsync(int currencyId, DateOnly date)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context
                        .ExchangeRates
                        .Where(x => x.CurrencyId == currencyId && x.Date <= date)
                        .OrderByDescending(x => x.Date)
                        .Select(x => new CurrencyRateDTO { CurrencyId = currencyId, Rate = x.Rate })
                        .FirstOrDefaultAsync();
        }
    }
}
