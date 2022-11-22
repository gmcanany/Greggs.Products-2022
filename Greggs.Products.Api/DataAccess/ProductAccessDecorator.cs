using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.DataAccess
{
    public class ProductAccessDecorator : IDataAccess<Product>
    {
        public readonly IDataAccess<Product> _provider;

        public ProductAccessDecorator(IDataAccess<Product> provider)
        {
            _provider = provider;
        }

        public virtual IEnumerable<Product> List(int? pageStart, int? pageSize)
        {
            return _provider.List(pageStart, pageSize);
        }
    }
}
