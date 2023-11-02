using Product.Entity;

namespace Product.Interfaces;

public interface IMaterialRepository: IRepositoryBase<Material>
{
    IQueryable<Material> GetAllMaterial(bool trackChanges);
    Material GetOneMaterial(int id,bool trackChanges);
    void CreateOneMaterial(Material material);
    void UpdateOneMaterial(Material material);
    void DeleteOneMaterial(Material material);
  
    
}