using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Updater.Data;
using Updater.Models;
// Не так выводит стоимость валют

namespace Updater.Services
{
    internal class CurrencyUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CurrencyUpdateService> _logger;
        private readonly int _intervalMinutes;
        private readonly string _currencyApiUrl;

        public CurrencyUpdateService(IServiceProvider serviceProvider, IConfiguration config, ILogger<CurrencyUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _intervalMinutes = config.GetValue<int>("UpdateIntervalMinutes", 1);
            _currencyApiUrl = config.GetValue<string>("CurrencyApiUrl") ?? throw new ArgumentNullException("CurrencyApiUrl не задан в конфигурации");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateCurrenciesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка обновлении валют");
                }

                await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes), stoppingToken);
            }
        }

        private async Task UpdateCurrenciesAsync()
        {
            _logger.LogInformation("Загрузка курсов валют");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_currencyApiUrl);

            response.EnsureSuccessStatusCode();

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var xml = Encoding.GetEncoding("windows-1251").GetString(bytes);

            var xdoc = XDocument.Parse(xml);


            var currencies = xdoc.Descendants("Valute")
                .Select(x => new Currency
                {
                    Name = x.Element("CharCode")!.Value,
                    Rate = decimal.Parse(x.Element("Value")!.Value, CultureInfo.GetCultureInfo("ru-RU"))
                })
                .ToList();



            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            foreach (var currency in currencies)
            {
                var existing = await db.Currency.FirstOrDefaultAsync(c => c.Name == currency.Name);
                if (existing == null)
                {
                    db.Currency.Add(currency);
                }
                else
                {
                    existing.Rate = currency.Rate;
                }
            }

            await db.SaveChangesAsync();
            _logger.LogInformation("Курсы валют успешно обновлены");
        }

    }
}
