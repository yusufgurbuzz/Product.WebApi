using System.Collections;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class ProductRepository : RepositoryBase<Entity.Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Entity.Product>>GetProduct(bool trackChanges)
    {
        return FindAll(trackChanges).OrderBy(b=>b.ProductId);
    }

    public async Task<Entity.Product>  GetProductById(int id, bool trackChanges)
    {
        return FindByCondition(b => b.ProductId.Equals(id), trackChanges).SingleOrDefault();
    }

    public async Task CreateProduct(Entity.Product product)
    {
        Create(product);
    }

    public async Task UpdateProduct(Entity.Product product)
    {
        var existingProduct = _context.Products.Find(product.ProductId);
        if (existingProduct != null)
        {
            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            _context.SaveChanges();
        }
    }

    public async Task DeleteProduct(Entity.Product product)
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