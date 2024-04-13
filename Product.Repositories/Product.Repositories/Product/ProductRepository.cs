using System.Collections;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Product.Entity;
using Product.Entity.RequestFeatures;
using Product.Interfaces;
using Product.Repositories.Extensions;

namespace Product.Repositories;

public sealed class ProductRepository : RepositoryBase<Entity.Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedList<Entity.Product>> GetProduct(ProductParameters productParameters, bool trackChanges)
    {
        var products = FindAll(trackChanges)
            .FilterProducts(productParameters.MinStock,productParameters.MaxStock)
            .SearchProducts(productParameters.SearchTerm)
            .OrderBy(b => b.ProductId);
        return PagedList<Entity.Product>.ToPagedList(products,
            productParameters.PageNumber,
            productParameters.Pagesize);
    }

    public async Task<Entity.Product> GetProductById(int id, bool trackChanges)
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