using CurrencyLoader.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyLoader.Context
{
    public class CurrencyContext: DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {
        }

        public DbSet<CurrencyDetails> CurrencyDetails { get; set; }
        public DbSet<CurrencyRates> CurrencyRates { get; set; }

    }
}
