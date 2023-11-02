namespace Product.Interfaces;

public interface IRepositoryManager
{
    IProductRepository ProductRepository { get; }
    IMaterialRepository MaterialRepository { get; }
    IStockRepository StockRepository { get; }
    void Save();
}