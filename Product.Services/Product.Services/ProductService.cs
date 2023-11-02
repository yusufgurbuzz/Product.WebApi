using Product.Interfaces;

namespace Product.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;

    }


    public IQueryable<Entity.Product> GetAllProduct(bool trackChanges)//services
    {
        return _repositoryManager.ProductRepository.GetAllProduct(trackChanges);//repo
    }

    public Entity.Product GetOneProduct(int id, bool trackChanges)
    {
        return _repositoryManager.ProductRepository.GetOneProduct(id, trackChanges);
    }

    public void CreateOneProduct(Entity.Product product)
    {
        if (product is null)
        {
            throw new ArgumentException(nameof(product));
        }
        
        _repositoryManager.ProductRepository.CreateOneProduct(product);
        _repositoryManager.Save();

    }

    public void UpdateOneProduct(int id,Entity.Product product, bool tranckChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetOneProduct(id, tranckChanges);
        if (entity is null)
        {
            throw new Exception($"Product with id : {id} could not found");
        }

        if (product is null)
        {
            throw new ArgumentException(nameof(product));
        }

        entity.ProductId = product.ProductId;
        entity.ProductName = product.ProductName;
        
        _repositoryManager.ProductRepository.UpdateOneProduct(entity);
        _repositoryManager.Save();

    }

    public void DeleteOneProduct(int id,bool trackChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetOneProduct(id, trackChanges);
        if (entity is null)
        {
            throw new Exception($"Product with id : {id} could not found");
        }
        
        _repositoryManager.ProductRepository.DeleteOneProduct(entity);
        _repositoryManager.Save();
    }
}