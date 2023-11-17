using Microsoft.EntityFrameworkCore.ChangeTracking;
using Product.Entity;
using Product.Interfaces;
using Product.Services;
using Product = Product.Entity.Product;

namespace Test.Methods.Unit.Test;
using Xunit;
using Moq;

public class ProductServiceTest
{
    [Fact]
    public void GetProduct_WhenCalled_ShouldReturnProducts()
    {
        var products = new List<global::Product.Entity.Product>
        {
            new global::Product.Entity.Product { ProductId = 1,ProductName = "Product1",ProductStock = 10},
            new global::Product.Entity.Product { ProductId= 2, ProductName = "Product2",ProductStock = 20}
           
        };
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo=>repo.ProductRepository.GetProduct(It.IsAny<bool>()))
            .Returns(products.AsQueryable());
        //AsQueryable bir IEnumerable koleksiyonunu IQueryable türüne dönüştürür. 

        var returnResult = productService.GetProduct(false).ToList();
        Assert.Equal(products.Count,returnResult.Count);

    }

    [Fact]
    public void GetProductById_WhenValidIdProvided_ShouldReturnProduct()
    {
        var products = new global::Product.Entity.Product
        {
             ProductId = 1,ProductName = "Product1",ProductStock = 10
        };
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        var mockProductRepository = new Mock<IProductRepository>();
        mockRepositoryManager.Setup(r => r.ProductRepository).Returns(mockProductRepository.Object);
        
        mockRepositoryManager.Setup(repo=>repo.ProductRepository.GetProductById(It.IsAny<int>(),It.IsAny<bool>()))
            .Returns(products);
        
        var returnResult = productService.GetProductById(1, false);
        
        Assert.NotNull(returnResult);
        Assert.Equal(products.ProductId, returnResult.ProductId);
        Assert.Equal(products.ProductName, returnResult.ProductName);
        Assert.Equal(products.ProductStock, returnResult.ProductStock);
    }
    [Fact]
    public void GetProductById_WhenInvalidIdProvided_ShouldReturnNull()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        
        //mockProductRepository adında bir Moq mock nesnesi oluşturuluyor.
        //Bu nesne, IProductRepository arayüzünü uygulayan bir sınıfın sahte bir örneğini temsil eder. Bu sayede, ProductService sınıfının ProductRepository özelliği üzerinde gerçek 
        //veritabanı erişimine gerek olmadan kontrol edilebilir bir ortam oluşturulmuş olur.
        var mockProductRepository = new Mock<IProductRepository>();
        
        mockRepositoryManager.Setup(repo => repo.ProductRepository).Returns(mockProductRepository.Object);

        mockProductRepository.Setup(repo => repo.GetProductById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns((global::Product.Entity.Product)null);

        // Act
        var result = productService.GetProductById(999, trackChanges: false);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreateOneProduct_WhenValidProductProvided_ShouldAddToRepository()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        var productToAdd = new global::Product.Entity.Product { ProductId = 1,ProductName = "product",ProductStock = 11};
        var mockProductRepository = new Mock<IProductRepository>();
        mockRepositoryManager.Setup(r => r.ProductRepository).Returns(mockProductRepository.Object);
        
        // Act
        mockRepositoryManager.Setup(repo => repo.ProductRepository.CreateProduct(It.IsAny<global::Product.Entity.Product> ()));
       
        productService.CreateProduct(productToAdd);

        // Assert
        mockRepositoryManager.Verify(repo => repo.Save());
    }
    [Fact]
    public void CreateOneProduct_WhenNullProductProvided_ShouldThrowArgumentException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => productService.CreateProduct(null));
    }
    [Fact]
    public void UpdateOneProduct_WhenValidIdAndProductProvided_ShouldUpdateProductInRepository()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        var existingProduct = new global::Product.Entity.Product
        {
            ProductId = 1,
            ProductName = "HaveProduct",
            ProductStock = 10
        };

        var updatedProduct = new global::Product.Entity.Product
        {
            ProductId = 1,
            ProductName = "UpdatedProduct",
            ProductStock = 20
        };

        mockRepositoryManager.Setup(repo => repo.ProductRepository.GetProductById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns(existingProduct);

        // Act
         productService.UpdateProductById(1, updatedProduct, true); // services

        // Assert
      
        mockRepositoryManager.Verify(repo => repo.ProductRepository.GetProductById(1, true));
        
        //repository
        mockRepositoryManager.Verify(repo => repo.ProductRepository.UpdateProduct(It.IsAny<global::Product.Entity.Product>()));
        
        mockRepositoryManager.Verify(repo => repo.Save());

        // Assert that the existing product is updated with the values of the updated product
        Assert.Equal(updatedProduct.ProductName, existingProduct.ProductName);
        Assert.Equal(updatedProduct.ProductStock, existingProduct.ProductStock);
    }
    
    [Fact]
    public void UpdateOneProduct_WhenInvalidIdProvided_ShouldThrowException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo => repo.ProductRepository.GetProductById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns((global::Product.Entity.Product)null); 
        //metodunun belirli parametrelerle çağrıldığında gerçek bir ürün döndürmek yerine, null değeri döndürmesini sağlar.

        // Act & Assert
        Assert.Throws<Exception>(() => productService.UpdateProductById(1, new global::Product.Entity.Product(), true));
    }

    [Fact]
    public void DeleteProduct_WithProductById_ShouldDeleteProduct()
    {
        var products = new global::Product.Entity.Product
        {
            ProductId = 1,ProductName = "Product1",ProductStock = 10
        };
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        var mockProductRepository = new Mock<IProductRepository>();
        mockRepositoryManager.Setup(r => r.ProductRepository).Returns(mockProductRepository.Object);
        
        mockRepositoryManager.Setup(repo=>repo.ProductRepository.GetProductById(It.IsAny<int>(),It.IsAny<bool>()))
            .Returns(products);//repo
        
          productService.DeleteProductById(1, true); //service
          
        mockRepositoryManager.Verify(repo => repo.Save());
    }

    [Fact]
    public void DeleteOneProduct_WhenInvalidIdProvided_ShouldThrowException()
    {
        // Arrange
        var mockRepositoryManager = new Mock<IRepositoryManager>();
        var productService = new ProductService(mockRepositoryManager.Object);

        mockRepositoryManager.Setup(repo => repo.ProductRepository.GetProductById(It.IsAny<int>(), It.IsAny<bool>()))
            .Returns((global::Product.Entity.Product)null); //id yok ise null dönecek

        // Act & Assert
        Assert.Throws<Exception>(() => productService.DeleteProductById(1, trackChanges: true));
    }




}

