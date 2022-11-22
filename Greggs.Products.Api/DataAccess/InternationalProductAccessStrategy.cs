using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greggs.Products.Api.DataAccess
{

    public class InternationalProductAccessStrategy : IDataAccess<InternationalProduct>
    {
        private static readonly IEnumerable<InternationalProduct> ProductDatabase = new List<InternationalProduct>()
        {
            new(new() { Name = "Sausage Roll", PriceInPounds = 1m }),
            new(new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m }),
            new(new() { Name = "Steak Bake", PriceInPounds = 1.2m }),
            new(new() { Name = "Yum Yum", PriceInPounds = 0.7m }),
            new(new() { Name = "Pink Jammie", PriceInPounds = 0.5m }),
            new(new() { Name = "Mexican Baguette", PriceInPounds = 2.1m }),
            new(new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m }),
            new(new() { Name = "Coca Cola", PriceInPounds = 1.2m })
        };

        public IEnumerable<InternationalProduct> List(int? pageStart, int? pageSize)
        {
            var queryable = ProductDatabase.AsQueryable();

            if (pageStart.HasValue)
                queryable = queryable.Skip(pageStart.Value);

            if (pageSize.HasValue)
                queryable = queryable.Take(pageSize.Value);

            return queryable.ToList();
        }

    }
}
