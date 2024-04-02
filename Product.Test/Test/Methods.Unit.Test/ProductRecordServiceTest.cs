using Product.Entity;
using Product.Interfaces;
using Product.Services;
using Xunit.Sdk;

namespace Test.Methods.Unit.Test;
using Moq;
using Xunit;

public class ProductRecordServiceTest
{/*
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
    public void ProduceProduct_WhenMaterialsCountIsLessThanThree_ShouldProduce()
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

    [Fact]
    public void ProduceProduct_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var mockProductRepository = new Mock<IProductRepository>();

        mockRepositoryManager.Setup(repo => repo.ProductRepository)
            .Returns(mockProductRepository.Object);

        var productRecordService = new ProductRecordService(mockRepositoryManager.Object);

        int productId = 80;
        int quantity = 10;

       
        mockProductRepository.Setup(repo => repo.GetProductById(productId, false))
            .Returns((Product.Entity.Product)null);

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => productRecordService.ProduceProduct(productId, quantity));
        Assert.Equal("Product not found", ex.Message);
    }

    [Fact]
    public void ProduceProduct_ShouldThrowException_WhenMaterialCountIsLessThan3()
    {
        int productId = 20;
        int quantity = 5;
        
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productionService = new ProductRecordService(mockRepositoryManager.Object);
        
        var productRecordService = new ProductRecordService(mockRepositoryManager.Object);
        mockRepositoryManager.Setup(repo => repo.ProductRepository.GetProductById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(new Product.Entity.Product());
        
        mockRepositoryManager.Setup(repo => repo.ProductMaterialRepository.GetProductMaterialsByProductId(It.IsAny<int>()))
            .Returns(new List<ProductMaterial> { new ProductMaterial() });
        
        var ex = Assert.Throws<Exception>(() => productRecordService.ProduceProduct(productId, quantity));
        Assert.Equal("At least 3 materials are required to produce the product",ex.Message);
        
    }*/

}