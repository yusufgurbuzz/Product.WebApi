using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Entity;
using Product.Interfaces;
using StackExchange.Redis;

namespace Draft.Product.WebAPI.Controllers;
[Route("api/[controller]"), ApiController]
public class DraftProduceProductController : Controller
{
    
    private readonly IServiceManager _serviceManager;
    private readonly ICacheService _cacheService;
    private readonly IProductMaterialRecordService _productMaterialRecordService;
    
    public DraftProduceProductController(IServiceManager serviceManager,ICacheService cacheService,
        IProductMaterialRecordService productMaterialRecordService,IConnectionMultiplexer connectionMultiplexer)
    {
        _serviceManager = serviceManager;
        _cacheService = cacheService;
        _productMaterialRecordService = productMaterialRecordService;
        
    }

    [HttpPost("productionRequest")]
    public IActionResult ProductProductionRequest(int productId, int quantity)
    {
       
        //Ürün üretim talebi verilerini productionData'ya doldurdum.ProductMaterialMap,ProductionMaterial ile map edildi.
        var productionData = new ProductMaterialMap
        {
            ProductId = productId,
            Quantity = quantity
        };
        
        // Taslak halinde olan verileri redisde 10 dakika tut
        var expiryTime = DateTimeOffset.Now.AddMinutes(10);
      var result =   _cacheService.SetData<ProductMaterialMap>($"product{productId}", productionData, expiryTime);
      return Ok();
        
    }

    [HttpPost("startProduction")]
    public IActionResult StartProduction(int approve, string keyValue) //üretimi onaylayıp üretmeye başlayan yer
    {
        if (approve == 1)
        {
            // Önbellekten onay bekleyen ürün üretim taleplerini al
            var keys = _cacheService.GetAllKeysTitle($"{keyValue}");

            foreach (var key in keys)
            {
                
                // JSON verisini ProductMaterialMap nesnesine dönüştürülür
                var productionRequest = _cacheService.GetData<ProductMaterialMap>(key);

                // Ürün üretim aşaması
                _serviceManager.ProductionRecordService.ProduceProduct(productionRequest.ProductId,
                    productionRequest.Quantity);

                // Ürün üretildi bellekte kaldırdık.
                _cacheService.RemoveData(key);
            }
        }
        else
        {
            _cacheService.RemoveData(keyValue);
        }

        return NoContent();

    }

    [HttpGet]
    public IActionResult GetProduction()
    {
        var redisData = _productMaterialRecordService.GetAllRedisData();
        return Ok(redisData);
    }
    [HttpGet ("key")]
    public IActionResult GetProductionByKey(string key)
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

    [HttpDelete("key")]
    public IActionResult RemoveProductionByKey(string key)
    {
         _cacheService.RemoveData(key);
         return NoContent();
    }
 
  


}