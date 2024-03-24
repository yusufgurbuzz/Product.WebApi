using Product.Entity;

namespace Product.Interfaces;

public interface IMaterialService
{
    IEnumerable<Material> GetMaterial(bool trackChanges);
    Material GetMaterialById(int id,bool trackChanges);
    void CreateMaterial(Material material);
    void UpdateMaterialById(int id,Material material,bool trackChanges);
    void DeleteMaterialById(int id,bool trackChanges);

}