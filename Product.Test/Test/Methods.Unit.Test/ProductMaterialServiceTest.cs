using Moq;
using Product.Entity;
using Product.Interfaces;
using Product.Repositories;
using Product.Services;

namespace Test.Methods.Unit.Test;

public class ProductMaterialServiceTest
{
    [Fact]
    public void GetProductMaterialsByProductId_ReturnsExpectedList()
    {
        // Arrange
        int productId = 5;
        var expectedProductMaterials = new List<ProductMaterial>
        {
            new ProductMaterial { ProductId = 5,MaterialId = 2, Quantity = 3},
            new ProductMaterial {  ProductId = 5,MaterialId = 3,Quantity = 5},
            new ProductMaterial {  ProductId = 5,MaterialId = 4,Quantity = 7},
            
        };

        var mockRepositoryManager = new Mock<IRepositoryManager>();
        mockRepositoryManager.Setup(repo => repo.ProductMaterialRepository.GetProductMaterialsByProductId(productId))
            .Returns(expectedProductMaterials);

        var productMaterialService = new ProductMaterialService(mockRepositoryManager.Object);

        // Act
        var result = productMaterialService.GetProductMaterialsByProductId(productId);

        // Assert
        Assert.Equal(expectedProductMaterials.Count, result.Count);

        for (int i = 0; i < expectedProductMaterials.Count; i++)
        {
            Assert.Equal(expectedProductMaterials[i].MaterialId, result[i].MaterialId);
            Assert.Equal(expectedProductMaterials[i].ProductId, result[i].ProductId);
          
        }
    }
    
    [Fact]
    public void AddProductMaterial_ShouldCallRepositoryMethod()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productMaterialService = new ProductMaterialService(mockRepositoryManager.Object);

        var productMaterialToAdd = new ProductMaterial
        {
           ProductId = 5,
           MaterialId = 2, 
           Quantity = 3
          
        };
        var mockProductRepository = new Mock<IProductMaterialRepository>();
        mockRepositoryManager.Setup(r => r.ProductMaterialRepository).Returns(mockProductRepository.Object);
        mockRepositoryManager.Setup(r=>r.ProductMaterialRepository.AddProductMaterial(It.IsAny<ProductMaterial> ()));
        
        // Act
        productMaterialService.AddProductMaterial(productMaterialToAdd);

        // Assert
        mockRepositoryManager.Verify(repo => repo.ProductMaterialRepository.AddProductMaterial(productMaterialToAdd));
    }
    
    
    
}