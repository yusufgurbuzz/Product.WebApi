using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Product.Entity;
using Product.Interfaces;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Product.Services;

public class CacheService : ICacheService
{
    private IDatabase _cacheDb;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public CacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
       // var redis = ConnectionMultiplexer.Connect("localhost:6379");
       _cacheDb = _connectionMultiplexer.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key); //key ile önbellekte depolanan bir dize değerini getirmek için kullanılır
        if (!String.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);  //JSON verisini C# nesnesine çevirir, T parametresi, dönüştürülecek veri türünü temsil eder. 
        }

        return default;
    }
    
    public T GetDataById<T>(int id)
    {
        var key = $"Product {id}"; 

        var value = _cacheDb.StringGet(key);

        if (!String.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expirtyTime = expirationTime - DateTimeOffset.Now;
        return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);
        //Redis önbellekte verinin saklanacağı anahtar (key) değerini temsil eder.
        //value değişkenini JSON formatına dönüştürerek önbelleğe saklar.
        //Bu, önbellekte saklanan verinin serileştirilmiş (JSON formatında) bir sürümünü temsil eder.
    }

    public object RemoveData(string key)
    {
        var _exist = _cacheDb.KeyExists(key);
        
        if (_exist)
        {
            return _cacheDb.KeyDelete(key);
        }

        return false;
    }
    

    public IEnumerable<string> GetAllKeysTitle(string title)
    {
        var keys = new List<string>();

        var server = _cacheDb.Multiplexer.GetServer(_cacheDb.Multiplexer.GetEndPoints()[0]); 

        foreach (var key in server.Keys(pattern: $"{title}*"))
        {
            keys.Add(key);
        }

        return keys;
    }

    public List<ProductMaterialDto> GetAllRedisData()
    {
        List<ProductMaterialDto> productMaterialMap = new List<ProductMaterialDto>();
        IDatabase redisDb = _connectionMultiplexer.GetDatabase();
        //Redis sunucusunun bir veya daha fazla uç noktasını alır ve ardından bunların ilkini seçer.
        var keys = redisDb.Multiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First()).Keys();
        var redisData = new List<string>();

        foreach (var key in keys)
        {
            var keyValue = redisDb.StringGet(key);
            var productMaterial = JsonConvert.DeserializeObject<ProductMaterialDto>(keyValue);
            productMaterialMap.Add(productMaterial);
        }

        return productMaterialMap;
    }
    public ProductMaterialDto GetRedisData(string key)
    {
        IDatabase redisDb = _connectionMultiplexer.GetDatabase();
        var redisData = redisDb.StringGet(key);
        var productMaterial = JsonConvert.DeserializeObject<ProductMaterialDto>(redisData);
        

        if (productMaterial is null)
        {
            return null;
        }

        return productMaterial;
    }

    
}