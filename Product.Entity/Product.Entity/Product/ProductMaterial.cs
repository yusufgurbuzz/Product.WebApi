using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Entity;

public class ProductMaterial
{
    
    public int  ProductMaterialId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int MaterialId { get; set; }
    public Material Material { get; set; }
    public int Quantity { get; set; } //ürün için kullanılan malzeme miktarı
}