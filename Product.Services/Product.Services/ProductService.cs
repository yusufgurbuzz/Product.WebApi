using Product.Interfaces;

namespace Product.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerService _loggerService;

    public ProductService(IRepositoryManager repositoryManager, ILoggerService loggerService)
    {
        _repositoryManager = repositoryManager;
        _loggerService = loggerService;
    }
    public IQueryable<Entity.Product> GetProduct(bool trackChanges)//services
    {
        return _repositoryManager.ProductRepository.GetProduct(trackChanges);//repo
    }

    public Entity.Product GetProductById(int id, bool trackChanges)
    {
        return _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
    }

    public void CreateProduct(Entity.Product product)
    {
        if (product is null)
        {
            throw new NullReferenceException();
        }
        if (_repositoryManager is null || _repositoryManager.ProductRepository is null)
        {
            throw new InvalidOperationException("Repository is not properly initialized.");
        }
        _repositoryManager.ProductRepository.CreateProduct(product);
        _repositoryManager.Save();

    }

    public void UpdateProductById(int id,Entity.Product product, bool tranckChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetProductById(id, tranckChanges);
        if (entity is null)
        {
            _loggerService.LogInfo($"Product with id : {id} could not found");
            throw new Exception($"Product with id : {id} could not found");
        }

        if (product is null)
        {
            throw new ArgumentNullException();
        }

        entity.ProductId = product.ProductId;
        entity.ProductName = product.ProductName;
        entity.ProductStock = product.ProductStock;
        
        _repositoryManager.ProductRepository.UpdateProduct(entity);
        _repositoryManager.Save();

    }

    public void DeleteProductById(int id,bool trackChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
        if (entity is null)
        {
            _loggerService.LogInfo($"Product with id : {id} could not found");
            throw new Exception($"Product with id : {id} could not found");
        }
        
        _repositoryManager.ProductRepository.DeleteProduct(entity);
        _repositoryManager.Save();
    }
}