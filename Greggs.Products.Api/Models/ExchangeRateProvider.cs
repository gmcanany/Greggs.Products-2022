﻿using System.Collections.Generic;
using System.Linq;

namespace Greggs.Products.Api.Models
{
    public sealed class ExchangeRateProvider
    {
        static readonly ExchangeRateProvider instance = new ExchangeRateProvider();

        private List<CurrencyRate> _rates;

        public ExchangeRateProvider()
        {
            // Load list of available servers
            _rates = new List<CurrencyRate>
            {
                new CurrencyRate{ Code = "GBP", Rate = 1m },
                new CurrencyRate{ Code = "EUR", Rate = 1.11m }
            };
        }

        public static ExchangeRateProvider Instance()
        {
            return instance;
        }

        public decimal GetRate(string Code)
        {
            return _rates.FirstOrDefault(x => x.Code == Code)!.Rate;
        }
    }
}
