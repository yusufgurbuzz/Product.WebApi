using System.Net;
using System.Net.Http.Json;
using Draft.Product.WebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductWebApi;
using static Product.Services.CacheService;

namespace Test.Endpoints;

public class DraftProduceProductControllerTest :IClassFixture<WebApplicationFactory<IDraftApiMarker>>
//WebApplicationFactory sınıfı, ASP.NET Core uygulamalarını test etmek için kullanılır.
//IDraftApiMarker tipi, genellikle API'yi tanımlayan bir işaretleme arayüzüdür.
//IClassFixture<WebApplicationFactory<IDraftApiMarker>> ile işaretlenmesi, bu sınıfın bir xUnit test sınıfı olduğunu ve
//WebApplicationFactory sınıfını kullanarak bir test sunucusunu başlatmak için bir fixture (düzenleyici) olarak kullanıldığını belirtir.

{
    private readonly WebApplicationFactory<IDraftApiMarker> _factory;
    private readonly HttpClient _client;
    public DraftProduceProductControllerTest(WebApplicationFactory<IDraftApiMarker> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    [Fact]
    public async Task ProductProductionRequest_ReturnsSuccessStatusCode()
    {
      
        var response = await _client.GetAsync("/api/DraftProduceProduct");

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);

    }

    [Fact]
    public async Task ProductProductionRequest_ReturnExistingId_ReturnsProduct()
    {
        string productkey = "product7"; //test ederken redise veri eklemeyi unutma
       
        var response = await _client.GetAsync($"/api/DraftProduceProduct?key={productkey}");
       
        var content = await response.Content.ReadAsStringAsync();
        var product = JsonConvert.DeserializeObject<List<Product.Entity.Product>>(content); 
        
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    

    [Fact] 
    public async Task RemoveProductionByKey_ReturnsNoContent()
    {
        var keyRemove = "product10";
        var response = await _client.DeleteAsync($"/api/DraftProduceProduct/key?key={keyRemove}");
        Assert.Equal(HttpStatusCode.NoContent,response.StatusCode);
    }
    
    [Fact]
    public async Task ProductProductionRequest_ReturnsOk()
    {
        // Arrange - Gerekli parametreleri belirle
        var productId = 7; // Örnek bir productId
        var quantity = 1; // Örnek bir quantity

        // Act
        var response = await _client.PostAsJsonAsync($"/api/DraftProduceProduct/productionRequest?productId={productId}&quantity={quantity}",new { productId, quantity });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
    }
    [Fact]
    public async Task StartProduction_ApproveIs1_ReturnsNoContent()
    {
        // Arrange 
        var approve = 1; 
        var keyValue = "product7"; 

        // Act
        //PostAsJsonAsync, bir HTTP POST isteğini belirtilen URI'ye JSON içeriği ile göndermek için kullanılan bir yöntemdir
        var response = await _client.PostAsJsonAsync($"/api/DraftProduceProduct/productionRequest?approve={approve}&keyValue={keyValue}",new { approve, keyValue });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); 

        
    }

    [Fact]
    public async Task StartProduction_ApproveIsNot1_ReturnsNoContent()
    {
       
        var approve = 0; 
        var keyValue = "product7"; //redisde var mı ??

        // Act
        var response = await _client.DeleteAsync($"/api/DraftProduceProduct/key?key={keyValue}");

        // Assert
         Assert.Equal(HttpStatusCode.NoContent, response.StatusCode); 

       
    }
    
    

}