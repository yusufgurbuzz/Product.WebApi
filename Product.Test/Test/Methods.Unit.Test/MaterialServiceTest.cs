using Product.Entity;
using Product.Interfaces;
using Product.Repositories;
using Product.Services;

namespace Test.Methods.Unit.Test;

using Xunit;
using Moq;
public class MaterialServiceTest
{
    [Fact]
    public void GetMaterial_WhenCalled_ShouldReturnProducts()
    {
        var material = new List <Material>
        {
            new Material { MaterialId = 1,MaterialName = "Metal",  MaterialUnit = 10, LastInTime = DateTime.Now},
            new Material { MaterialId = 2,MaterialName = "Çelik",  MaterialUnit = 60, LastInTime = DateTime.Now}
        };

        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var mockMaterialRepository = new Mock<IMaterialRepository>();
        var materialService = new MaterialService(mockRepositoryManager.Object);
        mockRepositoryManager.Setup(x => x.MaterialRepository).Returns(mockMaterialRepository.Object);
        
        mockRepositoryManager.Setup(r => r.MaterialRepository.GetAllMaterial(It.IsAny<bool>()))
            .Returns(material.AsQueryable());

        var returnResult = materialService.GetAllMaterial(false);
            
        Assert.Equal(material.Count,returnResult.Count());
    }

    [Fact]
    public void GetMaterialById_WhenValidIdProvided_ShouldReturnProduct()
    {
        var material = new Material()
        {
           MaterialId = 1, MaterialName = "Metal", MaterialUnit = 33,LastInTime = DateTime.Now
        };
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new MaterialService(mockRepositoryManager.Object);

        var mockProductRepository = new Mock<IMaterialRepository>();
        mockRepositoryManager.Setup(r => r.MaterialRepository).Returns(mockProductRepository.Object);
        
        mockRepositoryManager.Setup(repo=>repo.MaterialRepository.GetMaterialById(It.IsAny<int>(),It.IsAny<bool>()))
            .Returns(material);
        
        var returnResult = productService.GetMaterialById(1, false);
        
        Assert.NotNull(returnResult);
        Assert.Equal(material.MaterialId, returnResult.MaterialId);
        Assert.Equal(material.MaterialName, returnResult.MaterialName);
        Assert.Equal(material.MaterialUnit, returnResult.MaterialUnit);
        Assert.Equal(material.LastInTime, returnResult.LastInTime);
    }

    [Fact]
    public void GetMaterialById_WhenInvalidIdProvided_ShouldReturnNull()
    {
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var materialService = new MaterialService(mockRepositoryManager.Object);
        var mockMaterialRepository = new Mock<IMaterialRepository>();
        
        mockRepositoryManager.Setup(repo => repo.MaterialRepository).Returns(mockMaterialRepository.Object);

        mockMaterialRepository.Setup(repo => repo.GetMaterialById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns((Material)null);

        // Act
        var result = materialService.GetMaterialById(999, trackChanges: false);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public void CreateMaterial_WhenValidProductProvided_ShouldAddToRepository()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var materialService = new MaterialService(mockRepositoryManager.Object);

        var materialToAdd = new Material
        {
            MaterialId = 1 ,MaterialName = "Metal",MaterialUnit = 22, LastInTime = DateTime.Now
        };
        var mockMaterialRepository = new Mock<IMaterialRepository>();
        mockRepositoryManager.Setup(r => r.MaterialRepository).Returns(mockMaterialRepository.Object);
        //bir önceki satırdaki mock repository nesnesine nasıl tepki vereceğini belirten bir setup işlemidir.
        //Yani, MaterialRepository özelliği çağrıldığında ne yapılacağını tanımlar.
       // Returns(mockMaterialRepository.Object): Bu, MaterialRepository özelliği çağrıldığında geri döndürülecek değeri belirtir.
       // mockMaterialRepository.Object, mockMaterialRepository nesnesinin gerçek nesnesidir.
       // Bu, MaterialRepository'nin bir mock versiyonunu temsil eder ve bu mock repository'nin istenilen davranışları taklit etmesini sağlar.
        
        // Act
        mockRepositoryManager.Setup(repo => repo.MaterialRepository.CreateOneMaterial(It.IsAny<Material> ()));
       
        materialService.CreateOneMaterial(materialToAdd);

        // Assert
        mockRepositoryManager.Verify(repo => repo.Save());
    }
    [Fact]
    public void CreateMaterial_WhenNullProductProvided_ShouldThrowArgumentException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new MaterialService(mockRepositoryManager.Object);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => productService.CreateOneMaterial(null));
    }
    [Fact]
    public void UpdateMaterialById_WhenValidIdAndMaterialProvided_ShouldUpdateMaterialInRepository()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var materialService = new MaterialService(mockRepositoryManager.Object);

        var existingMaterial = new Material
        {
            MaterialId = 1,
            MaterialName = "Metal",
            MaterialUnit = 10
        };

        var updatedMaterial = new Material
        {
            MaterialId = 1,
            MaterialName = "Çelik",
            MaterialUnit = 20
        };

        mockRepositoryManager.Setup(repo => repo.MaterialRepository.GetMaterialById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(existingMaterial);

        // Act
        materialService.UpdateOneMaterial(1, updatedMaterial, true); // services

        // Assert
      
        mockRepositoryManager.Verify(repo => repo.MaterialRepository.GetMaterialById(1, true));
        
        //repository
        mockRepositoryManager.Verify(repo => repo.MaterialRepository.UpdateOneMaterial(It.IsAny<Material>()));
        
        mockRepositoryManager.Verify(repo => repo.Save());

        // Assert that the existing product is updated with the values of the updated product
        Assert.Equal(updatedMaterial.MaterialName, existingMaterial.MaterialName);
        Assert.Equal(updatedMaterial.MaterialUnit, existingMaterial.MaterialUnit);
    }
    
    [Fact]
    public void UpdateMaterial_WhenInvalidIdProvided_ShouldThrowException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var materialService = new MaterialService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo => repo.MaterialRepository.GetMaterialById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns((Material)null); 
        //metodunun belirli parametrelerle çağrıldığında gerçek bir ürün döndürmek yerine, null değeri döndürmesini sağlar.

        // Act & Assert
        Assert.Throws<Exception>(() => materialService.UpdateOneMaterial(1, new Material(), true));
    }
    
    [Fact]
    public void DeleteMaterial_WithMaterialById_ShouldDeleteMaterial()
    {
        var products = new Material
        {
            MaterialId = 1,MaterialName = "Çelik", MaterialUnit = 10, LastInTime = DateTime.Now
        };
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var materialService = new MaterialService(mockRepositoryManager.Object);

        var mockMaterialRepository = new Mock<IMaterialRepository>();
        mockRepositoryManager.Setup(r => r.MaterialRepository).Returns(mockMaterialRepository.Object);
        
        mockRepositoryManager.Setup(repo=>repo.MaterialRepository.GetMaterialById(It.IsAny<int>(),It.IsAny<bool>()))
            .Returns(products);//repo
        
        materialService.DeleteOneMaterial(1, true); //service
          
        mockRepositoryManager.Verify(repo => repo.Save());
    }
    
    [Fact]
    public void DeleteMaterial_WhenInvalidIdProvided_ShouldThrowException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var materialService = new MaterialService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo => repo.MaterialRepository.GetMaterialById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns((Material)null); //id yok ise null dönecek

        // Act & Assert
        Assert.Throws<Exception>(() => materialService.DeleteOneMaterial(1, trackChanges: true));
    }
    
}