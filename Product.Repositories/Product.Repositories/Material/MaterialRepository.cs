using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class MaterialRepository: RepositoryBase<Material>, IMaterialRepository
{
    public MaterialRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<Material> GetMaterial(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public Material GetMaterialById(int id, bool trackChanges)
    {
        return FindByCondition(b => b.MaterialId.Equals(id), trackChanges).SingleOrDefault();
    }

    public void CreateMaterial(Material material)
    {
        Create(material);
    }

    public void UpdateMaterial(Material material)
    {
        Update(material);
    }

    public void DeleteMaterial(Material material)
    {
        Delete(material);
    }

   
}