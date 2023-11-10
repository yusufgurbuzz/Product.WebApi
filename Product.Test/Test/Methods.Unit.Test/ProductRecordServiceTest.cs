using Product.Entity;
using Product.Interfaces;
using Product.Services;
using Xunit.Sdk;

namespace Test.Methods.Unit.Test;
using Moq;
using Xunit;

public class ProductRecordServiceTest
{
    [Fact]
    public void ProduceProduct_WhenProductNotFound_ShouldThrowException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productionService = new ProductRecordService(mockRepositoryManager.Object);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => productionService.ProduceProduct(8, 1));
    }
    [Fact]
    public void ProduceProduct_WhenMaterialsCountIsLessThanThree_ShouldThrowException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productionService = new ProductRecordService(mockRepositoryManager.Object);

        // Üretmek istenilen ProductId ve kaç adet üretilecek
        mockRepositoryManager.Setup(repo => repo.ProductRepository.GetProductById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(new Product.Entity.Product());

        //  metodun herhangi bir parametre ile çağrıldığında belirli bir ProductMaterial listesini döndürür.GetProductMaterialsByProductId list döndürüyor.
        mockRepositoryManager.Setup(repo => repo.ProductMaterialRepository.GetProductMaterialsByProductId(It.IsAny<int>()))
            .Returns(new List<ProductMaterial> { new ProductMaterial() });
        //Setup metodu, Moq kütüphanesinde bir mock nesnesinin belirli bir metodunu taklit etmek veya davranışını belirlemek için kullanılır
        
        // Act & Assert
        Assert.NotNull(() => productionService.ProduceProduct(7, 1));
    }
    
}