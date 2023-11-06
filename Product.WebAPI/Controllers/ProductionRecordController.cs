using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Product.Interfaces;

namespace ProductWebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class ProductionRecordController : Controller
{
    private readonly IServiceManager _serviceManager;
    
    public ProductionRecordController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public IActionResult ProduceProduct(int productId, int quantity)
    {
        try
        {
             _serviceManager.ProductionRecordService.ProduceProduct(productId, quantity);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}