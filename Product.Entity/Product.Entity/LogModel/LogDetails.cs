using System.Text.Json;

namespace Product.Entity.LogModel;

public class LogDetails
{
    public Object? ModelName { get; set; }
    public Object? Controller { get; set; }
    public Object? Action { get; set; }
    public Object? Id { get; set; }
    public Object? CreadAt { get; set; }

    public LogDetails()
    {
        CreadAt = DateTime.UtcNow;
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}