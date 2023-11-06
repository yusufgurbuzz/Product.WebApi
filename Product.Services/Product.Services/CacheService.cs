using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Product.Interfaces;
using StackExchange.Redis;

namespace Product.Services;

public class CacheService : ICacheService
{
    private IDatabase _cacheDb;

    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDb = redis.GetDatabase();
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
        var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
        return  _cacheDb.StringSet(key,JsonSerializer.Serialize(value),expirtyTime); 
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
}