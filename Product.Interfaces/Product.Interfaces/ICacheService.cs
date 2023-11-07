namespace Product.Interfaces;

public interface ICacheService
{
    T GetData<T>(string key);
    public T GetDataById<T>(int id);
    bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
    object RemoveData(String key);
    IEnumerable<string> GetAllKeysTitle(string title);

}