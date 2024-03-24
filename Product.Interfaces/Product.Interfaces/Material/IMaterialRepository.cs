using Product.Entity;

namespace Product.Interfaces;

public interface IMaterialRepository: IRepositoryBase<Material>
{
    IEnumerable<Material> GetMaterial(bool trackChanges);
    Material GetMaterialById(int id,bool trackChanges);
    void CreateMaterial(Material material);
    void UpdateMaterial(Material material);
    void DeleteMaterial(Material material);
  
    
}