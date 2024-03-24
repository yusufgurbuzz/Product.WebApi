using System.Linq.Expressions;
using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class ProductMaterialRepository :IProductMaterialRepository
{
    private readonly ApplicationDbContext _context;
    public ProductMaterialRepository(ApplicationDbContext context)
    {
       _context = context;
    }


    public List<ProductMaterial> GetProductMaterialsByProductId(int productId)
    {
        return _context.Products_Materials
            .Where(pm => pm.ProductId == productId)
            .ToList();
    }

    public void AddProductMaterial(ProductMaterial productMaterial)
    {
        if (productMaterial == null)
        {
            throw new ArgumentNullException(nameof(productMaterial));
        }

        _context.Products_Materials.Add(productMaterial);
        _context.SaveChanges();
    }
}