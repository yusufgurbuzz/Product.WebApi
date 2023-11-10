using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Product.Entity;

namespace Test.Endpoints;

public class MaterialControllerTest 
{
   
    private readonly HttpClient _client;
    
    public MaterialControllerTest()
    {

        _client = new HttpClient();
       
    }
    
    [Fact]
    public async Task GetAllMaterials_ReturnsSuccessStatusCode()
    {
        // Act
        _client.BaseAddress = new Uri("http://localhost:5062");
        var response = await _client.GetAsync("/api/Materials");

        // Assert
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    
    [Fact]
    public async Task GetOneMaterial_ExistingId_ReturnsMaterial()
    {
        try
        {
            // Arrange bölümde testler için gerekli tüm ön koşulların sağlanması gerekiyor. 
            int MaterialId = 2; // Var olan bir malzeme ID'si
            var requestUri = $"/api/Materials/{MaterialId}";
           
            // Act Test edeceğimiz birimi çalıştırdığımız bölümdür
            _client.BaseAddress = new Uri("http://localhost:5062");
            var response = await _client.GetAsync(requestUri);

            // Assert 

            var content = await response.Content.ReadAsStringAsync();
            var material = JsonConvert.DeserializeObject<Material>(content); 
            Assert.NotNull(material);   
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
        
    }

    [Fact]
    public async Task GetOneMaterial_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        int nonMaterialId = 999; // Var olmayan bir malzeme ID'si
        _client.BaseAddress = new Uri("http://localhost:5062");
        var requestUri = $"/api/Materials/{nonMaterialId}";

        // Act
        var response = await _client.GetAsync(requestUri);

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
        _client.BaseAddress = new Uri("http://localhost:5062");
        var response = await _client.PostAsync("/api/Materials", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdMaterial = JsonConvert.DeserializeObject<Material>(responseContent);
        
        Assert.NotNull(createdMaterial);
    }
    
    [Fact]
    public async Task DeleteOneMaterial_ReturnsNoContent() //foreignkey hatası gelebilir silinen material Id başka productId ile eşlenik olabilir
    {
        // Arrange
        // Silinecek malzemenin ID'si(veritabanında olmalı)
        int MaterialId = 20; 

        // Act
        _client.BaseAddress = new Uri("http://localhost:5062");
        var response = await _client.DeleteAsync($"/api/Materials/{MaterialId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
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
        _client.BaseAddress = new Uri("http://localhost:5062");
        var response = await _client.PutAsync($"/api/Materials/{MaterialId}", content);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode); 
        
    }
    
    

}