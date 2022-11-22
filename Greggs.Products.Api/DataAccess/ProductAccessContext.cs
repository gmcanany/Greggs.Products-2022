using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.DataAccess
{
    /// <summary>
    /// Strategy Context class for Product access
    /// </summary>
    public class ProductAccessContext
    {
        private IDataAccess<Product> _productAccessStrategy;

        public void SetStrategy(IDataAccess<Product> strategy)
        {
            _productAccessStrategy = strategy;
        }
        public IEnumerable<Product> ListProducts(int? pageStart, int? pageSize)
        {
            return _productAccessStrategy.List(pageStart, pageSize);
        }

    }
}
