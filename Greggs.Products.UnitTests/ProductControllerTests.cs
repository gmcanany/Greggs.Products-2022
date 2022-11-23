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
        // Arrange
        int pageStart = 0;
        int pageSize = 5;
        decimal exchangeRate = 1.11m;
        string[] sampleProducts = new[]
        {
            "Sausage Roll", "Vegan Sausage Roll", "Steak Bake","Yum Yum", "Pink Jammie"
        };
        var rng = new Random();
        var productList = Enumerable.Range(1, pageSize).Select(index => new Product()
            {
                PriceInPounds = rng.Next(0, 10),
                Name = sampleProducts[rng.Next(sampleProducts.Length)]
            })
            .ToArray();

        var loggerMock = new Mock<ILogger<ProductController>>();

        var productServiceMock = new Mock<IDataAccess<Product>>();
        productServiceMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>())).Returns(productList).Verifiable();

        var exchangeRateProviderMock = new Mock<IExchangeRateProvider>();
        exchangeRateProviderMock.Setup(x => x.GetRate(It.IsAny<string>())).Returns(exchangeRate).Verifiable();

        var sut = new ProductController(loggerMock.Object, productServiceMock.Object, exchangeRateProviderMock.Object);

        // Act
        var products = sut.Get(pageStart, pageSize);

        // Assert
        productServiceMock.Verify(x => x.List(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        products.Should().BeOfType<Product[]>();
    }


    [Fact]
    public async Task Given_Exchange_Rate_Product_List_Endpoint_Returns_Products_And_Prices()
    {
        int pageStart = 0;
        int pageSize = 5;
        decimal exchangeRate = 1.12m;

        // Arrange
        string[] sampleProducts = new[]
        {
            "Sausage Roll", "Vegan Sausage Roll", "Steak Bake","Yum Yum", "Pink Jammie"
        };
        var rng = new Random();
        var productList = Enumerable.Range(1, pageSize).Select(index => new Product()
            {
                PriceInPounds = rng.Next(0, 10),
                Name = sampleProducts[rng.Next(sampleProducts.Length)]
            })
            .ToArray();

        var loggerMock = new Mock<ILogger<ProductController>>();
        
        var productServiceMock = new Mock<IDataAccess<Product>>();
        productServiceMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>())).Returns(productList).Verifiable();

        var exchangeRateProviderMock = new Mock<IExchangeRateProvider>();
        exchangeRateProviderMock.Setup(x => x.GetRate(It.IsAny<string>())).Returns(exchangeRate).Verifiable();

        var sut = new ProductController(loggerMock.Object, productServiceMock.Object, exchangeRateProviderMock.Object);

        // Act
        var products = sut.Get(pageStart, pageSize);

        // Assert
        productServiceMock.Verify(x => x.List(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        products.Should().BeOfType<Product[]>();
        products.FirstOrDefault().Price.Should().Be(products.FirstOrDefault().PriceInPounds * exchangeRate);
    }
}