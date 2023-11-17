using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class ProductRepository : RepositoryBase<Entity.Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Entity.Product> GetProduct(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public Entity.Product GetProductById(int id, bool trackChanges)
    {
        return FindByCondition(b => b.ProductId.Equals(id), trackChanges).SingleOrDefault();
    }

    public void CreateProduct(Entity.Product product)
    {
        Create(product);
    }

    public void UpdateProduct(Entity.Product product)
    {
        Update(product);
    }

    public void DeleteProduct(Entity.Product product)
    {
        Delete(product);
    }

    public List<ProductMaterial> GetProductMaterialByProductId(int productId)
    {
        return _context.Products_Materials
            .Where(pm => pm.ProductId == productId)
            .ToList();
    }
}