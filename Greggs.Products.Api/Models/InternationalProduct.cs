namespace Greggs.Products.Api.Models
{
    public class InternationalProduct : ProductDecorator
    {
        private readonly decimal _exchangeRate;

        public InternationalProduct(Product product) : base(product)
        {
            _exchangeRate = ExchangeRateProvider.Instance().GetRate("EUR");
        }

        public decimal Price => Product.Price * _exchangeRate;

    }
}
