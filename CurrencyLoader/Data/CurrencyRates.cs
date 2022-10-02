using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyLoader.Data
{
    public class CurrencyRates
    {
        [Key]
        public int CurrencyRatesId { get; set; }
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }
        
        [ForeignKey("CurrencyDetailsId")]
        public int CurrencyDetailsId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }


    }
}
