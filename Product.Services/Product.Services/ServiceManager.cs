using Product.Interfaces;
using Product.Repositories;

namespace Product.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IMaterialService> _materialService;
    private readonly Lazy<IProductMaterialService> _productMaterialService;
    private readonly Lazy<IProductionRecordService> _productionRecordService;
 
    public ServiceManager( IRepositoryManager repositoryManager)
    {
        _productService = new Lazy<IProductService>(()=> new ProductService(repositoryManager));
        _materialService = new Lazy<IMaterialService>(()=> new MaterialService(repositoryManager));
        _productMaterialService = new Lazy<IProductMaterialService>(()=>new ProductMaterialService(repositoryManager));
        _productionRecordService = new Lazy<IProductionRecordService>(() => new ProductRecordService(repositoryManager));
       
    }

    public IProductService ProductService => _productService.Value;
    public IMaterialService MaterialService => _materialService.Value;
    public IProductMaterialService ProductMaterialService => _productMaterialService.Value;

    public IProductionRecordService ProductionRecordService => _productionRecordService.Value;
   
}