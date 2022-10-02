using CurrencyLoader.Context;
using CurrencyLoader.Data;
using CurrencyLoader.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyLoader
{
    public class CurrencyWorker : IHostedService, IDisposable
    {
        private readonly ILogger<CurrencyWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        private string url = "https://api.apilayer.com/fixer/latest?symbols=&base=";
        public CurrencyWorker(ILogger<CurrencyWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }



        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                 TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                var response = GetHttpRequestData();
                CurrencyModel currencyModel = JsonSerializer.Deserialize<CurrencyModel>(response.Content.ReadAsStringAsync().Result);
                AddCurrency(currencyModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private HttpResponseMessage GetHttpRequestData()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", "YOUR_API_KEY");
            return client.GetAsync(url).Result;
        }

        private void AddCurrency(CurrencyModel currencyModel)
        {
            try
            {
                CurrencyDetails currencyDetails = new CurrencyDetails();
                currencyDetails.baseCurrency = currencyModel.BaseCurrency;
                currencyDetails.date = currencyModel.date;
                currencyDetails.CurrencyRates = new List<CurrencyRates>();
                foreach (var currencyrate in currencyModel.rates)
                {
                    CurrencyRates currencyRates = new CurrencyRates();
                    currencyRates.CurrencyCode = currencyrate.Key;
                    currencyRates.Rate = currencyrate.Value;
                    currencyDetails.CurrencyRates.Add(currencyRates);
                }
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    CurrencyContext context = scope.ServiceProvider.GetRequiredService<CurrencyContext>();
                    context.Add(currencyDetails);
                    context.SaveChanges();
                    context.Dispose();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
