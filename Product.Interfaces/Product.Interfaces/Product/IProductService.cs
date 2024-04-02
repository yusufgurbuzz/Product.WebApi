using Product.Entity;

namespace Product.Interfaces;

public interface IProductService
{
      Task<IEnumerable<ProductDto>> GetProduct(bool trackChanges);
      Task<ProductDto> GetProductById(int id,bool trackChanges);
      Task<ProductDto> CreateProduct(ProductInsertionDto product);
      Task UpdateProductById(int id,UpdateProductDto product,bool trackChanges);
      Task DeleteProductById(int id,bool trackChanges);
      

}