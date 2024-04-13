using Product.Entity;
using Product.Entity.RequestFeatures;

namespace Product.Interfaces;


public interface IProductRepository : IRepositoryBase<Entity.Product>
{
    Task<PagedList<Entity.Product>> GetProduct(ProductParameters productParameters, bool trackChanges);
    Task<Entity.Product> GetProductById(int id, bool trackChanges);
    Task CreateProduct(Entity.Product product);
    Task UpdateProduct(Entity.Product product);
    Task DeleteProduct(Entity.Product product);
    List<ProductMaterial> GetProductMaterialByProductId(int productId);

}