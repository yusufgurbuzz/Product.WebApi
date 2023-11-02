using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class StockRepository : RepositoryBase<Stock>,  IStockRepository
{
    private readonly ApplicationDbContext _context;
    public StockRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Stock> GetAllStocks(bool trackChanges)
    {
        return FindAll(trackChanges);
    }

    public Stock GetOneStock(int id, bool trackChanges)
    {
        return FindByCondition(b => b.StockId.Equals(id), trackChanges).SingleOrDefault();
    }

    public void CreateOneStock(Stock stock)
    {
        Create(stock);
    }

    public void UpdateOneStock(Stock stock)
    {
        Update(stock);
    }

    public void DeleteOneStock(Stock stock)
    {
        Delete(stock);
    }

  
}