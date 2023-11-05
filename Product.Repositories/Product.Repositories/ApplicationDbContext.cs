using Microsoft.EntityFrameworkCore;
using Product.Entity;

namespace Product.Repositories;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions context) : base(context)
    {
        
    }

    public DbSet<Material> Materials { get; set; } 
    public DbSet<ProductMaterial> Products_Materials { get; set; }
    public DbSet<Entity.Product> Products { get; set; }
    public DbSet<ProductionRecord> ProductionRecord { get; set; }

}