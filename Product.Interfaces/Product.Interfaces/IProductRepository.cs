using Product.Entity;

namespace Product.Interfaces;


public interface IProductRepository : IRepositoryBase<Entity.Product>
{
    IQueryable<Entity.Product> GetProduct(bool trackChanges);
    Entity.Product GetProductById(int id, bool trackChanges);
    void CreateProduct(Entity.Product product);
    void UpdateProduct(Entity.Product product);
    void DeleteProduct(Entity.Product product);
    List<ProductMaterial> GetProductMaterialByProductId(int productId);

}