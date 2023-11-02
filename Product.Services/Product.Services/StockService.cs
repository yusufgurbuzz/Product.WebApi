
using Product.Entity;
using Product.Interfaces;
using Product.Repositories;

namespace Product.Services;

public class StockService: IStockService
{
    private readonly IRepositoryManager _repositoryManager;
   
    public StockService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
       
    }  
    
    public IQueryable<Stock> GetAllStocks(bool trackChanges)
    {
        return _repositoryManager.StockRepository.GetAllStocks(trackChanges);
    }
    
    public Stock GetOneStock(int id, bool trackChanges)
    {
        return _repositoryManager.StockRepository.GetOneStock(id, trackChanges);
    }

    public void CreateOneStock(Stock stock)
    {
        {
            if (stock is null)
            {
                throw new ArgumentException(nameof(stock));
            }
        
            _repositoryManager.StockRepository.CreateOneStock(stock);
            _repositoryManager.Save();

        }
    }

    public void UpdateOneStock(int id, Stock stock, bool trackChanges)
    {
        var entity = _repositoryManager.StockRepository.GetOneStock(id, trackChanges);
        if (entity is null)
        {
            throw new Exception($"Stock with id : {id} could not found");
        }

        if (stock is null)
        {
            throw new ArgumentException(nameof(stock));
        }
        entity.StockId = stock.StockId;
        entity.MaterialId = stock.MaterialId;
        entity.StockAmount = stock.StockAmount;
        entity.LastInTime = stock.LastInTime;
        entity.LastOutTime = stock.LastOutTime;
        
        
        _repositoryManager.StockRepository.UpdateOneStock(entity);
        _repositoryManager.Save();
    }

    public void DeleteOneStock(int id, bool trackChanges)
    {
        var entity = _repositoryManager.StockRepository.GetOneStock(id,trackChanges);
        if (entity is null)
        {
            throw new Exception($"Stock with id : {id} could not found");
        }
        
        _repositoryManager.StockRepository.DeleteOneStock(entity);
        _repositoryManager.Save();
    }
}