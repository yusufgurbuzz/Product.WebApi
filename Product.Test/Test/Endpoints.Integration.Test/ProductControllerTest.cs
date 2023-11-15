using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using ProductWebApi;


namespace Test.Endpoints;

public class ProductControllerTest : IClassFixture<WebApplicationFactory<IApiMarker>>
{
   private readonly WebApplicationFactory<IApiMarker> _factory;
   private readonly HttpClient _httpClient;

   public ProductControllerTest(WebApplicationFactory<IApiMarker> factory)
   {
       _factory = factory;
       _httpClient = _factory.CreateClient();

   }
   [Fact]
   public async Task GetProducts_ReturnsSuccessStatusCode()
   {
       
       var response = await _httpClient.GetAsync("/api/Products");

       Assert.Equal(HttpStatusCode.OK,response.StatusCode);

   }

   [Fact]
   public async Task GetProductById_ReturnExistingId_ReturnsProduct()
   {
       int productId = 5;
      
       var response = await _httpClient.GetAsync($"/api/Products/{productId}");
       
       var content = await response.Content.ReadAsStringAsync();
       var product = JsonConvert.DeserializeObject<Product.Entity.Product>(content); 
       Assert.NotNull(product); 
       
       Assert.Equal(HttpStatusCode.OK,response.StatusCode);
   }
   
   [Fact]
   public async Task GetProductById_ReturnNonExistingId_ReturnsProduct()
   {
       int nonProductId = 999;
       var response = await _httpClient.GetAsync($"/api/Products/{nonProductId}");
     
       Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); 
   }

   [Fact]
   public async Task CreateProduct_ReturnOk()
   {
       var product = new Product.Entity.Product
       {
           ProductName = "Bilgisayar",
           ProductStock = 15
           
       };
       var jsonContent = JsonConvert.SerializeObject(product);
       var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
       
       
       var response = await _httpClient.PostAsync("/api/Products",content);
       
       var responseContent = await response.Content.ReadAsStringAsync();
       var createdProduct = JsonConvert.DeserializeObject<Product.Entity.Product>(responseContent);
       
       Assert.NotNull(createdProduct);
       Assert.Equal(HttpStatusCode.OK,response.StatusCode);
       
   }
   [Fact]
   public async Task DeleteProductById_ReturnNoContent()
   {
       int productId = 17; // böyle bir Id veritabanında var mı?
       
       var responseDelete = await _httpClient.DeleteAsync($"/api/Products/{productId}");

       Assert.Equal(HttpStatusCode.NoContent, responseDelete.StatusCode);
   }

   [Fact]
   public async Task UpdateMaterialById_ReturnNoContent()
   {
       int productId = 7;
       var newProductData = new Product.Entity.Product
       {
         ProductId = 7,
           ProductName = "Kalem",
           ProductStock = 5
       };
       var content = new StringContent(JsonConvert.SerializeObject(newProductData), Encoding.UTF8, "application/json");
       
       var response = await _httpClient.PutAsync($"/api/Products/{productId}",content);
       
       Assert.Equal(HttpStatusCode.NoContent,response.StatusCode);
   }
}
   
   
   





