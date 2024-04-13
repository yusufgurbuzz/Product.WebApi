using System.Dynamic;
using Product.Entity;
using Product.Entity.RequestFeatures;

namespace Product.Interfaces;

public interface IProductService
{
      Task<(IEnumerable<ExpandoObject>,MetaData metaData)> GetProduct(ProductParameters productParameters,bool trackChanges);
      Task<ProductDto> GetProductById(int id,bool trackChanges);
      Task<ProductDto> CreateProduct(ProductInsertionDto product);
      Task UpdateProductById(int id,UpdateProductDto product,bool trackChanges);
      Task DeleteProductById(int id,bool trackChanges);
      

}