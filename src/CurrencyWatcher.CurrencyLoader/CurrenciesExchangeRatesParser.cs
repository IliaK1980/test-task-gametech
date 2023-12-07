using CurrencyWatcher.Domain.Models;

namespace CurrencyWatcher.CurrencyLoader
{
    internal class CurrenciesExchangeRatesParser : ICurrenciesExchangeRatesParser
    {
        public Currency[] ParseCurrenciesExchangeRates(Stream currenciesStream)
        {
            using var streamReader = new StreamReader(currenciesStream);

            var headers = (streamReader.ReadLine()?.Split('|')) ?? throw new Exception("Input stream contains no data");

            if (headers[0] != "Date")
            {
                throw new Exception("Inpud data has incorrect format");
            }

            var result = new Currency[headers.Length - 1];

            for (int i = 1; i < headers.Length; i++)
            {
                result[i - 1] = new Currency { Code = headers[i].Trim() };
            }

            while (streamReader.Peek() > 0)
            {
                var data = streamReader.ReadLine()?.Split('|');

                if (data != null)
                {
                    var date = DateOnly.ParseExact(data[0], "dd.MM.yyyy");

                    for (int i = 1; i < data.Length; i++)
                    {
                        var exchangeRate = new ExchangeRate { Date = date, Rate = decimal.Parse(data[i]) };

                        result[i - 1].Rates.Add(exchangeRate);
                    }
                }
            }

            return result;
        }
    }
}
