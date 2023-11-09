using Microsoft.AspNetCore.Mvc;
using Product.Interfaces;
using Product.Repositories;
using StackExchange.Redis;

namespace Draft.Product.WebAPI.Controllers;
[Route("api/[controller]"), ApiController]
public class ProductMaterialController : Controller
{
    private readonly IProductMaterialRecordService _productMaterialRecordService;
   
    public ProductMaterialController(IProductMaterialRecordService productMaterialRecordService)
    {
        _productMaterialRecordService = productMaterialRecordService;
    }

    [HttpGet]
    public IActionResult GetAllProductMaterial()
    {
        var redisData = _productMaterialRecordService.GetAllRedisData();

        return Ok(redisData);
    }
    [HttpGet ("key")]
    public IActionResult GetProductMaterial(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return BadRequest("Key is required.");
        }

        var redisData = _productMaterialRecordService.GetRedisData(key);

        if (redisData == null)
        {
            return NotFound($"Key '{key}' not found in Redis.");
        }

        return Ok(redisData);
    }
    
    [HttpPost]
    public void CreateProductMaterial()
    {
        _productMaterialRecordService.ProductMaterialsToRedis();
        Ok();
    }


}