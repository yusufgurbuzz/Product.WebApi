namespace Product.Interfaces;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IMaterialService MaterialService { get; }
    IStockService StockService { get; }
}