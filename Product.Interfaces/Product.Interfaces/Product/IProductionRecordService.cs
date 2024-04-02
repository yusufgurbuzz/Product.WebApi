namespace Product.Interfaces;

public interface IProductionRecordService
{
    public Task ProduceProduct(int productId, int Quantity);
  
}