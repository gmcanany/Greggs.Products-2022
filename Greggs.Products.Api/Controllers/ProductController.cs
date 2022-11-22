using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private static readonly string[] Products = new[]
    {
        "Sausage Roll", "Vegan Sausage Roll", "Steak Bake", "Yum Yum", "Pink Jammie"
    };

    private readonly ILogger<ProductController> _logger;
    //private readonly IDataAccess<Product> _productService;
    private readonly ProductAccessContext _productAccessContext;

    public ProductController(ILogger<ProductController> logger/*, IDataAccess<Product> productService*/)
    {
        _logger = logger;
        _productAccessContext = new ProductAccessContext();
        //_productService = productService;

    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {
        _logger.LogInformation($"Get products called with pageStart {pageStart}, pageSize {pageSize}.");

        try
        {
            if (pageSize > Products.Length)
                pageSize = Products.Length;

            _productAccessContext.SetStrategy(new InternationalProductAccessStrategy());

            var products = _productAccessContext.ListProducts(pageStart, pageSize);

            //var products = _productService.List(pageStart, pageSize);

            return products.ToArray();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error while fetching product list with pageStart {pageStart}, pageSize {pageSize}");
            return new List<Product>().ToArray();
        }

    }
}