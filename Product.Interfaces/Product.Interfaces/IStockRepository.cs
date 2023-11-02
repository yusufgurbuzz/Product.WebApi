using Product.Entity;

namespace Product.Interfaces;

public interface IStockRepository
{
    IQueryable<Stock> GetAllStocks(bool trackChanges);
    Stock GetOneStock(int id,bool trackChanges);
    void CreateOneStock(Stock stock);
    void UpdateOneStock(Stock stock);
    void DeleteOneStock(Stock stock);
    
}