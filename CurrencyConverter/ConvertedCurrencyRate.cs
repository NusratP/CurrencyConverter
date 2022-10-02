using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CurrencyConverter
{
    public class ConvertedCurrencyRate
    {
        [JsonPropertyName("result")]

        public double Result { get; set; }
        [JsonPropertyName("rates")]
        public IDictionary<string, double> Rates { get; set; }
    }
}
