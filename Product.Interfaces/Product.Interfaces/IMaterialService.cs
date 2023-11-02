using Product.Entity;

namespace Product.Interfaces;

public interface IMaterialService
{
    IQueryable<Material> GetAllMaterial(bool trackChanges);
    Material GetOneMaterial(int id,bool trackChanges);
    void CreateOneMaterial(Material material);
    void UpdateOneMaterial(int id,Material material,bool trackChanges);
    void DeleteOneMaterial(int id,bool trackChanges);

}