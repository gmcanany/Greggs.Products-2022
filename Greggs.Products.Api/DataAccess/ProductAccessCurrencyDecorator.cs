using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.DataAccess
{
    public class ProductAccessCurrencyDecorator : ProductAccessDecorator
    {

        public ProductAccessCurrencyDecorator(IDataAccess<Product> provider): base (provider)
        {
        }

        public IEnumerable<Product> List(int? pageStart, int? pageSize)
        {
            return _provider.List(pageStart, pageSize);
        }
    }
}
