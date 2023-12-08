namespace CurrencyWatcher.CurrencyLoader.Tests
{
    public class CurrenciesExchangeRatesParserTests
    {
        [Fact]
        public void ParseCurrenciesExchangeRates_Success()
        {
            // arrange
            var parser = new CurrenciesExchangeRatesParser();

            // act
            using var stream = GetCurrenciesStream("currencies.txt");
            var currencies = parser.ParseCurrenciesExchangeRates(stream);

            // assert
            Assert.NotNull(currencies);
            Assert.True(currencies.Length == 31);
            Assert.True(currencies[0].Rates.Count == 4);
        }

        [Fact]
        public void ParseCurrenciesExchangeRates_ThrowsException()
        {
            // arrange
            var parser = new CurrenciesExchangeRatesParser();

            // act & assert
            using var stream = GetCurrenciesStream("invalid_currencies.txt");
            Assert.Throws<Exception>(() => parser.ParseCurrenciesExchangeRates(stream));
        }

        private Stream GetCurrenciesStream(string fileName)
        {
            var path = Path.GetRelativePath(Directory.GetCurrentDirectory(), $"Data\\{fileName}");

            Stream fs = File.OpenRead(path);
            return fs;

        }
    }
}