using System.Runtime.InteropServices.JavaScript;

namespace Product.Entity;

public class Stock
{
    public int StockId { get; set; }
    public int MaterialId { get; set; }
    public int StockAmount { get; set; }
    public DateTime? LastInTime { get; set; }
    public DateTime? LastOutTime { get; set; }
}