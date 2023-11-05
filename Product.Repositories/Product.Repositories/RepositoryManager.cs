using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private Lazy<IProductRepository> _productRepository;
    private Lazy<IMaterialRepository> _materialRepository;
    private Lazy<IProductMaterialRepository> _productMaterialRepository;
    private Lazy<IProductionRecordRepository> _productionRecordRepository;
    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context;
        _productRepository = new Lazy<IProductRepository>(()=> new ProductRepository(_context));
        _materialRepository = new Lazy<IMaterialRepository>(() => new MaterialRepository(_context));
        _productMaterialRepository =
            new Lazy<IProductMaterialRepository>(() => new ProductMaterialRepository(_context));
        _productionRecordRepository = new Lazy<IProductionRecordRepository>(()=>new ProductionRecordRepository(_context));
    }
    
    public IProductRepository ProductRepository => _productRepository.Value;
    public IMaterialRepository MaterialRepository => _materialRepository.Value;
    public IProductMaterialRepository ProductMaterialRepository => _productMaterialRepository.Value;
    public IProductionRecordRepository ProductionRecordRepository => _productionRecordRepository.Value;

    public void Save()
    {
        _context.SaveChanges();
    }
}