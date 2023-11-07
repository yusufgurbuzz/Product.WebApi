namespace Product.Interfaces;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IMaterialService MaterialService { get; }
    IProductMaterialService ProductMaterialService { get; }
   IProductionRecordService ProductionRecordService  { get; }
   
   
   
}