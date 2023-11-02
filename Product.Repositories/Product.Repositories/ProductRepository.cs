using Product.Interfaces;

namespace Product.Repositories;

public class ProductRepository : RepositoryBase<Entity.Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Entity.Product> GetAllProduct(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public Entity.Product GetOneProduct(int id, bool trackChanges)
    {
        return FindByCondition(b => b.ProductId.Equals(id), trackChanges).SingleOrDefault();
    }

    public void CreateOneProduct(Entity.Product product)
    {
        Create(product);
    }

    public void UpdateOneProduct(Entity.Product product)
    {
        Update(product);
    }

    public void DeleteOneProduct(Entity.Product product)
    {
        Delete(product);
    }
}