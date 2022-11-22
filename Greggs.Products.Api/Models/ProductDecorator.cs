namespace Greggs.Products.Api.Models
{
    public class ProductDecorator : Product
    {
        protected readonly Product Product;

        public ProductDecorator(Product product)
        {
            Product = product;
        }

        public override decimal Price()
        {
            return Product.Price();
        }
    }
}
