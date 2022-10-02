using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyLoader.Data
{
    public class CurrencyDetails
    {
        [Key]
        public int CurrencyDetailsId { get; set; }
        public string baseCurrency { get; set; }
        public DateTime date { get; set; }
        public IList<CurrencyRates> CurrencyRates { get; set; }

    }
}
