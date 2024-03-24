using Product.Entity;

namespace Product.Interfaces;

public interface IProductService
{
      Task<IEnumerable<Entity.Product>> GetProduct(bool trackChanges);
      Entity.Product GetProductById(int id,bool trackChanges);
      void CreateProduct(Entity.Product product);
      void UpdateProductById(int id,UpdateProductDto product,bool trackChanges);
      void DeleteProductById(int id,bool trackChanges);
      

}