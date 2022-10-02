using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace CurrencyConverter
{
    internal class Program
    {

        static string baseUrl = "https://api.apilayer.com/fixer/";
        static void Main(string[] args)
        {
            Console.Write("Enter from currency: ");
            var fromCurrency = Console.ReadLine();
            Console.Write("Enter to currency: ");
            var toCurrency = Console.ReadLine();
            Console.Write("Enter amount: ");
            var amount = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter date(yyyy-MM-dd) to get conversion for that day else press enter if you want latest conversion: ");
            var date = Console.ReadLine();

            if (!string.IsNullOrEmpty(date))
            {
                ConvertCurrencyForSpecificDate(fromCurrency, toCurrency, amount, date);
            }
            else
            {
                ConvertLiveCurrency(fromCurrency, toCurrency, amount);
            }
            Console.ReadLine();
        }


        public static void ConvertCurrencyForSpecificDate(string fromCurrency, string toCurrency, double amount, string date)
        {
            try
            {
                string url = $"{baseUrl}{date}?symbols={toCurrency}&from={fromCurrency}";
                var response = GetHttpRequestData(url);
                var obj = JsonSerializer.Deserialize<ConvertedCurrencyRate>(response.Content.ReadAsStringAsync().Result);
                var convertedAmount = obj.Rates.Values.FirstOrDefault() * amount;
                Console.Write("The result of currency conversion is: ");
                Console.WriteLine($"{amount} {fromCurrency} = {convertedAmount} {toCurrency}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertLiveCurrency(string fromCurrency, string toCurrency, double amount)
        {
            try
            {
                string url = $"{baseUrl}convert?to={toCurrency}&from={fromCurrency}&amount={amount}";
                var response = GetHttpRequestData(url);
                var obj = JsonSerializer.Deserialize<ConvertedCurrencyRate>(response.Content.ReadAsStringAsync().Result);
                Console.Write("The result of currency conversion is: ");
                Console.WriteLine($"{amount} {fromCurrency} = {obj.Result} {toCurrency}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static HttpResponseMessage GetHttpRequestData(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", "YOUR_API_KEY");
            return client.GetAsync(url).Result;
        }
    }
}