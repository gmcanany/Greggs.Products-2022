using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.DataAccess
{
    public class ProductAccessDecorator : IDataAccess<Product>
    {
        public readonly IDataAccess<Product> Component;

        public ProductAccessDecorator(IDataAccess<Product> component)
        {
            Component = component;
        }

        public virtual IEnumerable<Product> List(int? pageStart, int? pageSize)
        {
            return Component.List(pageStart, pageSize);
        }
    }
}
