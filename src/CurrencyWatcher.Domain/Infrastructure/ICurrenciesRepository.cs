using CurrencyWatcher.Domain.Models;

namespace CurrencyWatcher.Domain.Infrastructure
{
    public interface ICurrenciesRepository
    {
        Task<CurrencyDTO[]> GetAllCurrenciesAsync();
        Task<CurrencyRateDTO?> GetCurrencyRateAsync(int currencyId, DateOnly date);
        Task SaveCurrenciesAsync(Currency[] currencies);
    }
}