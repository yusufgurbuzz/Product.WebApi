using Product.Entity;

namespace Product.Interfaces;

public interface IStockService
{
    IQueryable<Stock> GetAllStocks(bool trackChanges);
    Stock GetOneStock(int id,bool trackChanges);
    void CreateOneStock(Stock stock);
    void UpdateOneStock(int id,Stock stock,bool trackChanges);
    void DeleteOneStock(int id,bool trackChanges);

    
}