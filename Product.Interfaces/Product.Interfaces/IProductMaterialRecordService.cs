namespace Product.Interfaces;

public interface IProductMaterialRecordService
{
   void ProductMaterialsToRedis();
    List<string> GetAllRedisData();
    string GetRedisData(string key);
}