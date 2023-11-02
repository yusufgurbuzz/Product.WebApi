using Microsoft.EntityFrameworkCore;
using Product.Entity;

namespace Product.Repositories;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions context) : base(context)
    {
        
    }

    public DbSet<Material> Materials { get; set; }
    //public DbSet<Product_Material> Products_Materials { get; set; }
    public DbSet<Entity.Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }

}