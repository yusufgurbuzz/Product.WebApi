namespace Product.Interfaces;


public interface IProductRepository : IRepositoryBase<Entity.Product>
{
    IQueryable<Entity.Product> GetAllProduct(bool trackChanges);
    Entity.Product GetOneProduct(int id,bool trackChanges);
    void CreateOneProduct(Entity.Product product);
    void UpdateOneProduct(Entity.Product product);
    void DeleteOneProduct(Entity.Product product);
    
}