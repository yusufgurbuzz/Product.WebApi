using Product.Entity;

namespace Product.Interfaces;

public interface IProductionRecordRepository
{
    public List<ProductionRecord> GetProductionRecords();
    public void AddProductionRecord(ProductionRecord productionRecord);

}