using Product.Entity;

namespace Product.Repositories;

public interface IProductMaterialRepository
{
    public List<ProductMaterial> GetProductMaterialsByProductId(int productId);
    public void AddProductMaterial(ProductMaterial productMaterial);

}