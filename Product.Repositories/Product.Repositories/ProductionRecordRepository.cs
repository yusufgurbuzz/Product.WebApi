using Product.Entity;
using Product.Interfaces;

namespace Product.Repositories;

public class ProductionRecordRepository: RepositoryBase<ProductionRecord>,IProductionRecordRepository
{
     private readonly ApplicationDbContext _context;
     public ProductionRecordRepository(ApplicationDbContext context) : base(context)
     {
         _context = context;
     }
    
    public List<ProductionRecord> GetProductionRecords()
    {
        return _context.ProductionRecord.ToList();
    }

    public void AddProductionRecord(ProductionRecord productionRecord)
    {
        if (productionRecord == null)
        {
            throw new ArgumentNullException(nameof(productionRecord));
        }

        _context.ProductionRecord.Add(productionRecord);
        _context.SaveChanges();
    }
   
}