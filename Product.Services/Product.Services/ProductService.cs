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

    public Entity.Product GetProductById(int id, bool trackChanges)
    {
        return _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
    }

    public void CreateOneProduct(Entity.Product product)
    {
        if (product is null)
        {
            throw new NullReferenceException();
        }
        if (_repositoryManager is null || _repositoryManager.ProductRepository is null)
        {
            throw new InvalidOperationException("Repository is not properly initialized.");
        }
        _repositoryManager.ProductRepository.CreateOneProduct(product);
        _repositoryManager.Save();

    }

    public void UpdateOneProduct(int id,Entity.Product product, bool tranckChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetProductById(id, tranckChanges);
        if (entity is null)
        {
            throw new Exception($"Product with id : {id} could not found");
        }

        if (product is null)
        {
            throw new ArgumentNullException();
        }

        entity.ProductId = product.ProductId;
        entity.ProductName = product.ProductName;
        entity.ProductStock = product.ProductStock;
        
        _repositoryManager.ProductRepository.UpdateOneProduct(entity);
        _repositoryManager.Save();

    }

    public void DeleteOneProduct(int id,bool trackChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
        if (entity is null)
        {
            throw new Exception($"Product with id : {id} could not found");
        }
        
        _repositoryManager.ProductRepository.DeleteOneProduct(entity);
        _repositoryManager.Save();
    }
}