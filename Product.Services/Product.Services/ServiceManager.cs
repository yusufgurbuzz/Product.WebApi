using Product.Interfaces;
using Product.Repositories;

namespace Product.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IMaterialService> _materialService;
    private readonly Lazy<IStockService> _stockService;
   
    
    public ServiceManager( IRepositoryManager repositoryManager)
    {
        _productService = new Lazy<IProductService>(()=> new ProductService(repositoryManager));
        _materialService = new Lazy<IMaterialService>(()=> new MaterialService(repositoryManager));
        _stockService = new Lazy<IStockService>(()=> new StockService(repositoryManager));
    }

    public IProductService ProductService => _productService.Value;
    public IMaterialService MaterialService => _materialService.Value;
    public IStockService StockService => _stockService.Value;
}