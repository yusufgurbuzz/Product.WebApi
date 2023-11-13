using Product.Entity;

namespace Product.Services;
using Product.Interfaces;
public class MaterialService : IMaterialService
{
    private readonly IRepositoryManager _repositoryManager;

    public MaterialService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;

    }    
    
    public IQueryable<Material> GetAllMaterial(bool trackChanges)
    {
        return _repositoryManager.MaterialRepository.GetAllMaterial(trackChanges);
    }

    public Material GetMaterialById(int id, bool trackChanges)
    {
        return _repositoryManager.MaterialRepository.GetMaterialById(id, trackChanges);
    }

    public void CreateOneMaterial(Material material)
    {
        if (material is null)
        {
            throw new NullReferenceException(nameof(material));
        }
        
        _repositoryManager.MaterialRepository.CreateOneMaterial(material);
        _repositoryManager.Save();

    }
    

    public void UpdateOneMaterial(int id, Material material, bool trackChanges)
    {
        var entity = _repositoryManager.MaterialRepository.GetMaterialById(id,trackChanges);
        if (entity is null)
        {
            throw new Exception($"Material with id : {id} could not found");
        }

        if (material is null)
        {
            throw new ArgumentException(nameof(material));
        }

        entity.MaterialId = material.MaterialId;
        entity.MaterialName = material.MaterialName;
        entity.MaterialUnit = material.MaterialUnit;
        entity.LastInTime = DateTime.UtcNow;
        
        _repositoryManager.MaterialRepository.UpdateOneMaterial(entity);
        _repositoryManager.Save();
    }

    public void DeleteOneMaterial(int id, bool trackChanges)
    {
        var entity = _repositoryManager.MaterialRepository.GetMaterialById(id,trackChanges);
        if (entity is null)
        {
            throw new Exception($"Material with id : {id} could not found");
        }
        
        _repositoryManager.MaterialRepository.DeleteOneMaterial(entity);
        _repositoryManager.Save();
    }
}