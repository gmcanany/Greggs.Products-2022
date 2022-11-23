using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess
{
    public abstract class DecoratorFactory : IDecoratorFactory
    {
        public abstract IDataAccess<Product> CreateDataAccessDecorator(IDataAccess<Product> dataAccess);
    }

    public class CurrencyDecoratorFactory : DecoratorFactory
    {

        public override IDataAccess<Product> CreateDataAccessDecorator(IDataAccess<Product> dataAccess)
        {
            return new ProductAccessCurrencyDecorator(dataAccess, ExchangeRateProvider.Instance());
        }
    }

    public interface IDecoratorFactory
    {
        IDataAccess<Product> CreateDataAccessDecorator(IDataAccess<Product> dataAccess);
    }
}
