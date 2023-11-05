using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class MaterialRepository: RepositoryBase<Material>, IMaterialRepository
{
    public MaterialRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<Material> GetAllMaterial(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public Material GetMaterialById(int id, bool trackChanges)
    {
        return FindByCondition(b => b.MaterialId.Equals(id), trackChanges).SingleOrDefault();
    }

    public void CreateOneMaterial(Material material)
    {
        Create(material);
    }

    public void UpdateOneMaterial(Material material)
    {
        Update(material);
    }

    public void DeleteOneMaterial(Material material)
    {
        Delete(material);
    }

   
}