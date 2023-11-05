using Product.Entity;
using Product.Interfaces;

namespace Product.Services;

public class ProductMaterialService : IProductMaterialService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductMaterialService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;

    } 
    
    public List<ProductMaterial> GetProductMaterialsByProductId(int productId)
    {
        return _repositoryManager.ProductMaterialRepository.GetProductMaterialsByProductId(productId);
    }

    public void AddProductMaterial(ProductMaterial productMaterial)
    {
        _repositoryManager.ProductMaterialRepository.AddProductMaterial(productMaterial);
    }
}