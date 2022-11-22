using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using FluentAssertions;

namespace Greggs.Products.UnitTests;

public class ProductControllerTests
{
   
    [Fact]
    public async Task Get_Calls_DataAccessStrategy_ListProducts_Returns_Products()
    {
        int pageStart = 0;
        int pageSize = 5;
        string[] sampleProducts = new[]
        {
            "Sausage Roll", "Vegan Sausage Roll", "Steak Bake","Yum Yum", "Pink Jammie"
        };
        var rng = new Random();
        var productList = Enumerable.Range(1, pageSize).Select(index => new InternationalProduct(new Product()
            {
                PriceInPounds = rng.Next(0, 10),
                Name = sampleProducts[rng.Next(sampleProducts.Length)]
            }))
            .ToArray();

        var loggerMock = new Mock<ILogger<ProductController>>();
        var strategyMock = new Mock<IDataAccess<InternationalProduct>>();
        strategyMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>())).Returns(productList).Verifiable();

        var sut = new ProductController(loggerMock.Object, strategyMock.Object/*, productServiceMock.Object*/);

        var products = sut.Get(pageStart, pageSize);

        strategyMock.Verify(x => x.List(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        products.Should().BeOfType<Product[]>();
    }

}