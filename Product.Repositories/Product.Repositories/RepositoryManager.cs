using Product.Interfaces;

namespace Product.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _Context;
    private Lazy<IProductRepository> _productRepository;
    private Lazy<IMaterialRepository> _materialRepository;
    private Lazy<IStockRepository> _stockRepository;

    public RepositoryManager(ApplicationDbContext Context)
    {
        _Context = Context;
        _productRepository = new Lazy<IProductRepository>(()=> new ProductRepository(_Context));
        _materialRepository = new Lazy<IMaterialRepository>(() => new MaterialRepository(_Context));
        _stockRepository = new Lazy<IStockRepository>(() => new StockRepository(_Context));
    }
    
    public IProductRepository ProductRepository => _productRepository.Value;
    public IMaterialRepository MaterialRepository => _materialRepository.Value;
    public IStockRepository StockRepository => _stockRepository.Value;

    public void Save()
    {
        _Context.SaveChanges();
    }
}