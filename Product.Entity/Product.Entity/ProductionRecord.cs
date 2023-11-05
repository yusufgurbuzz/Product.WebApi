namespace Product.Entity;

public class ProductionRecord
{
    public int ProductionRecordId { get; set; }
    public int Quantity { get; set; } // üretilen yeni ürün sayısı
    public DateTime? ProductionDate { get; set; }
    public int ProductId { get; set; }
}