using Product.Entity;

namespace Product.Interfaces;

public interface IProductMaterialRecordService
{
   void ProductMaterialsToRedis();
    
   List<ProductMaterialMap> GetAllRedisData();
   ProductMaterialMap GetRedisData(string key);
   
}