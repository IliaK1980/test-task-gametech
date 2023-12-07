using CurrencyWatcher.CurrencyLoader;
using CurrencyWatcher.DataAccess;
using CurrencyWatcher.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateHostBuilder(args).Build();

using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    await services.GetRequiredService<App>().Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

HostApplicationBuilder CreateHostBuilder(string[] args)
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddDbContextFactory<CurrenciesContext>(
        options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CurrenciesDb")));

    builder.Services
        .AddScoped<ICurrencyService, CurrencyService>()
        .AddScoped<ICurrenciesExchangeRatesProvider, CurrenciesExchangeRatesProvider>()
        .AddScoped<ICurrenciesExchangeRatesParser, CurrenciesExchangeRatesParser>()
        .AddScoped<ICurrenciesRepository, CurrenciesRepository>()
        .AddSingleton<App>();

    builder.Services.Configure<ExchangeRatesProviderOptions>(builder.Configuration.GetSection(ExchangeRatesProviderOptions.ExchangeRatesProvider));

    return builder;
}
