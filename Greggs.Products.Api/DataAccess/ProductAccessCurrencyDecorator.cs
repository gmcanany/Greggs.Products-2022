using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.DataAccess
{
    public class ProductAccessCurrencyDecorator : ProductAccessDecorator
    {
        private readonly decimal _exchangeRate;

        public ProductAccessCurrencyDecorator(IDataAccess<Product> component): base (component)
        {
            _exchangeRate = ExchangeRateProvider.Instance().GetRate("EUR");
        }

        public override IEnumerable<Product> List(int? pageStart, int? pageSize)
        {
            var products = Component.List(pageStart, pageSize);

            foreach (var product in products)
            {
                product.Price = product.PriceInPounds * _exchangeRate;
            }

            return products;
        }
    }
}
