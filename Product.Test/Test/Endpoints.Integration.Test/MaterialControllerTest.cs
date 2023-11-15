using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Product.Entity;
using ProductWebApi;

namespace Test.Endpoints;

public class MaterialControllerTest : IClassFixture<WebApplicationFactory<IApiMarker>>
{
   
    private readonly WebApplicationFactory<IApiMarker> _factory;
    private readonly HttpClient _httpClient;
    
    public MaterialControllerTest(WebApplicationFactory<IApiMarker> factory)
    {

        _factory = factory;
        _httpClient = _factory.CreateClient();
       
    }
    
    [Fact]
    public async Task GetAllMaterials_ReturnsSuccessStatusCode()
    {
        // Act
       
        var response = await _httpClient.GetAsync("/api/Materials");

        // Assert
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    
    [Fact]
    public async Task GetOneMaterial_ExistingId_ReturnsMaterial()
    {
        int materialId = 2;
      
        var response = await _httpClient.GetAsync($"/api/Materials/{materialId}");
       
        var content = await response.Content.ReadAsStringAsync();
        var material = JsonConvert.DeserializeObject<Material>(content); 
        Assert.NotNull(material); 
       
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async Task GetOneMaterial_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        int nonMaterialId = 999; // Var olmayan bir malzeme ID'si
        
        // Act
        var response = await _httpClient.GetAsync($"/api/Materials/{nonMaterialId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task CreateOneMaterial_ReturnsCreated()
    {
        // Arrange
        
        var newMaterial = new Material
        {
          
            MaterialName = "lastik",
            MaterialUnit = 10,
            LastInTime = null
        };

     
        var jsonContent = JsonConvert.SerializeObject(newMaterial);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
      
        var response = await _httpClient.PostAsync("/api/Materials", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdMaterial = JsonConvert.DeserializeObject<Material>(responseContent);
        
        Assert.NotNull(createdMaterial);
    }
    
    [Fact]
    public async Task DeleteMaterial_ReturnsNoContent()
    {
        int materialId = 21; //veritabanında var mı?
        var getResponse = await _httpClient.GetAsync($"/api/Materials/{materialId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task UpdateOneMaterial_ReturnsNoContent()
    {
        // Arrange
        var MaterialId = 2; // veritabanında Güncellenecek Id
        var updatedMaterial = new Material
        { 
            MaterialId = 2,
            MaterialName = "Pil",
            MaterialUnit = 190,
            LastInTime = null
        };

        var content = new StringContent(JsonConvert.SerializeObject(updatedMaterial), Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PutAsync($"/api/Materials/{MaterialId}", content);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode); 
        
    }
    
    

}