using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Product.Entity;
using Product.Interfaces;
using Product.Repositories;
using StackExchange.Redis;

namespace Product.Services;

public class ProductMaterialRecordService : IProductMaterialRecordService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    
    public  ProductMaterialRecordService(ApplicationDbContext context,IConnectionMultiplexer connectionMultiplexer)
    {
        _dbContext = context;
        _connectionMultiplexer = connectionMultiplexer;
    }

    public void ProductMaterialsToRedis()
    {
        var productMaterials = _dbContext.Products_Materials
            .Include(pm => pm.Product)
            .Include(pm => pm.Material)
            .Select(pm => new
                {
                    ProductMaterialId = pm.ProductMaterialId,
                    ProductId = pm.ProductId,
                    MaterialId = pm.MaterialId,
                    ProductName = pm.Product.ProductName,
                    ProductStock = pm.Product.ProductStock,
                    MaterialName = pm.Material.MaterialName,
                    MaterialUnit = pm.Material.MaterialUnit
                }
            ).ToList();

        var redis = _connectionMultiplexer.GetDatabase();
        foreach (var productMaterial in productMaterials)
        {
            var key = $"ProductMaterial:{productMaterial.ProductMaterialId}";
            var serializedValue = JsonConvert.SerializeObject(productMaterial);
            redis.StringSet(key, serializedValue, TimeSpan.FromHours(1));
            
        }
    }
  
    public List<ProductMaterialMap> GetAllRedisData()
    {
        List<ProductMaterialMap> productMaterialMap = new List<ProductMaterialMap>();
        IDatabase redisDb = _connectionMultiplexer.GetDatabase();
        //Redis sunucusunun bir veya daha fazla uç noktasını alır ve ardından bunların ilkini seçer.
        var keys = redisDb.Multiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First()).Keys();
        var redisData = new List<string>();

        foreach (var key in keys)
        {
            var keyValue = redisDb.StringGet(key);
            var productMaterial = JsonConvert.DeserializeObject<ProductMaterialMap>(keyValue);
            productMaterialMap.Add(productMaterial);
        }

        return productMaterialMap;
    }

    public ProductMaterialMap GetRedisData(string key)
    {
        IDatabase redisDb = _connectionMultiplexer.GetDatabase();
        var redisData = redisDb.StringGet(key);
        var productMaterial = JsonConvert.DeserializeObject<ProductMaterialMap>(redisData);
        

        if (productMaterial is null)
        {
            return null;
        }

        return productMaterial;
    }
}