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
    
    public IQueryable<Material> GetMaterial(bool trackChanges)
    {
        return _repositoryManager.MaterialRepository.GetMaterial(trackChanges);
    }

    public Material GetMaterialById(int id, bool trackChanges)
    {
        return _repositoryManager.MaterialRepository.GetMaterialById(id, trackChanges);
    }

    public void CreateMaterial(Material material)
    {
        if (material is null)
        {
            throw new NullReferenceException(nameof(material));
        }
        
        _repositoryManager.MaterialRepository.CreateMaterial(material);
        _repositoryManager.Save();

    }
    

    public void UpdateMaterialById(int id, Material material, bool trackChanges)
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
        
        _repositoryManager.MaterialRepository.UpdateMaterial(entity);
        _repositoryManager.Save();
    }

    public void DeleteMaterialById(int id, bool trackChanges)
    {
        var entity = _repositoryManager.MaterialRepository.GetMaterialById(id,trackChanges);
        if (entity is null)
        {
            throw new Exception($"Material with id : {id} could not found");
        }
        
        _repositoryManager.MaterialRepository.DeleteMaterial(entity);
        _repositoryManager.Save();
    }
}