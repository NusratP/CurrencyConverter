using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CurrencyLoader.Model
{
    public class CurrencyModel
    {
        [JsonPropertyName("base")]
        public string BaseCurrency { get; set; }
        public DateTime date { get; set; }
        public IDictionary<string, double> rates { get; set; }

    }
    
}
