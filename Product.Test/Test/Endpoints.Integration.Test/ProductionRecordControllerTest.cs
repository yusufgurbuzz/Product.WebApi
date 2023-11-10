using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Product.Entity;
using ProductWebApi;

namespace Test.Endpoints;

public class ProductionRecordControllerTest:IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly WebApplicationFactory<IApiMarker> _factory;
    private readonly HttpClient _client;

    public ProductionRecordControllerTest(WebApplicationFactory<IApiMarker> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

    }
    [Fact]
    public async Task ProduceProduct_ReturnsOk()
    {
        // Arrange

       var productId = 7;
       var quantity = 1;

        // Act
        var response = await _client.PostAsJsonAsync($"/api/ProductionRecord?productId={productId}&quantity={quantity}", new { productId, quantity });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
    }

    [Fact]
    public async Task ProductionRecord_ProductNotFound_ReturnsNotFound()
    {
        // Arrange
        var productId = 999; // Bu ürün ID'si mevcut olmayan bir ürün Id
        var quantity = 10; // Üretilen ürün miktarı

        var requestBody = new
        {
            productId,
            quantity
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/ProductionRecord/ProduceProduct", content);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); 
    }
    
    
    //İdsi olunca üreten testi de yaz..
    

}