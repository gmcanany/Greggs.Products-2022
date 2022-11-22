namespace Greggs.Products.Api.Models;

public class Product // : AbstractProduct
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
    public virtual decimal Price()
    {
        return PriceInPounds;
    }
}