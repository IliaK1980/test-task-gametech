namespace CurrencyWatcher.Domain.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public List<ExchangeRate> Rates { get; } = [];
    }
}
