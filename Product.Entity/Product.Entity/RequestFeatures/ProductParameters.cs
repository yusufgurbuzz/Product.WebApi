namespace Product.Entity.RequestFeatures;

public class ProductParameters : RequestParam
{
    public uint MinStock { get; set; }

    public uint MaxStock { get; set; } = 1000;

    public bool ValidStockRange => MaxStock > MinStock;

    public String? SearchTerm { get; set; }

    public ProductParameters()
    {
        OrderBy = "ProductName";
    }
}