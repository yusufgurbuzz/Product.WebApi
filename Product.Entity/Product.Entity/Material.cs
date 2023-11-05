namespace Product.Entity;

public class Material
{
    public int MaterialId { get; set; }
    public string MaterialName { get; set; }
    public int MaterialUnit { get; set; } // material sayısı
    public DateTime? LastInTime { get; set; }
}