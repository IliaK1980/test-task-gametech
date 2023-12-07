namespace CurrencyWatcher.Domain.Models
{
    public class ExchangeRate
    {
        public int CurrencyId { get; set; }

        public DateOnly Date { get; set; }

        public decimal Rate { get; set; }

        public Currency Currency { get; set; }
    }
}
