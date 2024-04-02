using Product.Repositories;

namespace Product.Interfaces;

public interface IRepositoryManager
{
    IProductRepository ProductRepository { get; }
    IMaterialRepository MaterialRepository { get; }
    IProductMaterialRepository ProductMaterialRepository { get; }
    IProductionRecordRepository ProductionRecordRepository  { get; }
    Task Save();
}