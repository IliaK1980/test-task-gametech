using CurrencyWatcher.Domain.Models;

namespace CurrencyWatcher.DataAccess.Tests
{
    public class CurrenciesRepositoryTests
    {
        private static readonly Currency[] _validCurrencies =
        [
            new()
            {
                Code = "Test 1",
                Rates =
                [
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rate = 1
                    },
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                        Rate = 1.1m
                    }
                ]
            },
            new()
            {
                Code = "Test 2",
                Rates =
                [
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rate = 1
                    },
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                        Rate = 1.1m
                    }
                ]
            }
        ];

        private static readonly Currency[] _invalidCurrencies =
        [
            new()
            {
                Code = "Test 1",
                Rates =
                [
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rate = 1
                    },
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rate = 1.1m
                    }
                ]
            },
            new()
            {
                Code = "Test 1",
                Rates =
                [
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Rate = 1
                    },
                    new ExchangeRate
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                        Rate = 1.1m
                    }
                ]
            }
        ];

        [Theory, MemberData(nameof(ValidCurrenciesData))]
        public async void SaveCurrenciesAsync_Success(Currency[] currencies)
        {
            // arrange
            var repository = new CurrenciesRepository(new TestDbContextFactory());

            // act & assert
            await repository.SaveCurrenciesAsync(currencies);
        }

        [Theory, MemberData(nameof(InvalidCurrenciesData))]
        public async void SaveCurrenciesAsync_ThrowsException(Currency[] currencies)
        {
            // arrange
            var repository = new CurrenciesRepository(new TestDbContextFactory());

            // act & assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.SaveCurrenciesAsync(currencies));
        }

        [Theory, MemberData(nameof(ValidCurrenciesData))]
        public async void GetCurrencyRateAsync_Success(Currency[] currencies)
        {
            // arrange
            var repository = new CurrenciesRepository(new TestDbContextFactory());
            await repository.SaveCurrenciesAsync(currencies);

            // act
            var actualExchangeRate = await repository.GetCurrencyRateAsync(1, DateOnly.FromDateTime(DateTime.Now));

            // assert
            Assert.True(actualExchangeRate != null);
            Assert.Equal(_validCurrencies[0].Rates[0].Rate, actualExchangeRate.Rate);
        }

        [Theory, MemberData(nameof(ValidCurrenciesData))]
        public async void GetCurrencyRateAsync_Success_OlderDate(Currency[] currencies)
        {
            // arrange
            var repository = new CurrenciesRepository(new TestDbContextFactory());
            await repository.SaveCurrenciesAsync(currencies);

            // act
            var actualExchangeRate = await repository.GetCurrencyRateAsync(1, DateOnly.FromDateTime(DateTime.Now.AddDays(1)));

            // assert
            Assert.True(actualExchangeRate != null);
            Assert.Equal(_validCurrencies[0].Rates[0].Rate, actualExchangeRate.Rate);
        }

        [Theory, MemberData(nameof(ValidCurrenciesData))]
        public async void GetCurrencyRateAsync_ReturnsNull(Currency[] currencies)
        {
            // arrange
            var repository = new CurrenciesRepository(new TestDbContextFactory());
            await repository.SaveCurrenciesAsync(currencies);

            // act
            var actualExchangeRate = await repository.GetCurrencyRateAsync(3, DateOnly.FromDateTime(DateTime.Now));

            // assert
            Assert.Null(actualExchangeRate);
        }

        public static IEnumerable<object[]> ValidCurrenciesData =>
            new List<object[]>
            {
                new object[] { _validCurrencies }
            };

        public static IEnumerable<object[]> InvalidCurrenciesData =>
            new List<object[]>
            {
                new object[] { _invalidCurrencies }
            };
    }
}
