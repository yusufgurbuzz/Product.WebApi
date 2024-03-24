using Product.Entity;

namespace Product.Interfaces;

public interface IProductMaterialService
{
    public List<ProductMaterial> GetProductMaterialsByProductId(int productId);
    public void AddProductMaterial(ProductMaterial productMaterial);
}