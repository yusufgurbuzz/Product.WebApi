using AutoMapper;
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

    private async Task<Entity.Product> GetProductByIdCheckExists(int id, bool trackChanges)
    {
        var product = await _repositoryManager.ProductRepository.GetProductById(id, trackChanges);
        if (product is null)
            throw new ProductNotFoundException(id);
        
        return product;
    }

    public async Task<IEnumerable<ProductDto>> GetProduct(bool trackChanges)
    {
        var productsQuery = await _repositoryManager.ProductRepository.GetProduct(trackChanges);
        return  _mapper.Map<IEnumerable<ProductDto>>(productsQuery);
    }

    public async Task<ProductDto> GetProductById(int id, bool trackChanges)
    {
        var product = await GetProductByIdCheckExists(id,trackChanges);
        return  _mapper.Map<ProductDto>(product);
    }
    public async Task<ProductDto> CreateProduct(ProductInsertionDto product)
    {
        var entity = _mapper.Map<Entity.Product>(product);
        
        await _repositoryManager.ProductRepository.CreateProduct(entity);
        await _repositoryManager.Save();
        
        return _mapper.Map<ProductDto>(entity);

    }

    public async Task UpdateProductById(int id,UpdateProductDto product, bool trackChanges)
    {
        var entity = await GetProductByIdCheckExists(id,trackChanges);;
        entity = _mapper.Map<Entity.Product>(product);
        
        await _repositoryManager.ProductRepository.UpdateProduct(entity);
        await _repositoryManager.Save();

    }

    public async Task DeleteProductById(int id,bool trackChanges)
    {
        var entity = await GetProductByIdCheckExists(id,trackChanges);
        await _repositoryManager.ProductRepository.DeleteProduct(entity);
        await _repositoryManager.Save();
    }
}