using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Entity;
using Product.Entity.Exceptions;
using Product.Interfaces;

namespace Product.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;
    public ProductService(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerService = loggerService;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Entity.Product>> GetProduct(bool trackChanges)
    {
        var productsQuery = _repositoryManager.ProductRepository.GetProduct(trackChanges);
        return productsQuery;
    }

    public Entity.Product GetProductById(int id, bool trackChanges)
    {
        var product = _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        return product;
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

    public void UpdateProductById(int id,UpdateProductDto product, bool tranckChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetProductById(id, tranckChanges);
        if (entity is null)
        {
            throw new ProductNotFoundException(id);
        }

        if (product is null)
        {
            throw new ArgumentNullException();
        }

        entity = _mapper.Map<Entity.Product>(product);
        
        _repositoryManager.ProductRepository.UpdateProduct(entity);
        _repositoryManager.Save();

    }

    public void DeleteProductById(int id,bool trackChanges)
    {
        var entity = _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
        if (entity is null)
        {
            throw new ProductNotFoundException(id);
        }
        _repositoryManager.ProductRepository.DeleteProduct(entity);
        _repositoryManager.Save();
    }
}