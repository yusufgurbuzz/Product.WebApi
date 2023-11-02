using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Product.Entity;
using Product.Interfaces;

namespace ProductWebApi.Controllers;

[Route("api/Stokcs"), ApiController]
public class StockController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    
    public StockController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpGet]
    public IActionResult GetAllStocks()
    {
        var stocks = _serviceManager.StockService.GetAllStocks(false);//service
        return Ok(stocks);
    }
    [HttpGet ("{id:int}")]
    public IActionResult GetOneStock(int id)
    {
        var stock = _serviceManager.StockService.GetOneStock(id,false);
        if (stock is null)
        {
            return NotFound();
        }
        
        return Ok(stock);
    }
    
    [HttpPost]
    public IActionResult CreateOneStock([FromBody] Stock stock)
    {
        try
        {
            if (stock is null)
            {
                return BadRequest();
            }
            _serviceManager.StockService.CreateOneStock(stock);
            return StatusCode(201,stock);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
 
    [HttpDelete ("{id:int}")]
    public IActionResult DeleteOneStock(int id)
    {
        try
        {
            _serviceManager.StockService.DeleteOneStock(id,false);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

 

    [HttpPut ("{id:int}")]
        public void UpdateOneStock(int id, Stock stock, int addStockAmount)
        {
       
            
            var entity = _serviceManager.StockService.GetOneStock(id, true);
            if (entity is null)
            {
                throw new Exception($"Stock with id : {id} could not found");
            }

            if (stock is null)
            {
                throw new ArgumentException(nameof(stock));
            }

            var totalCount = entity.StockAmount + addStockAmount;
            entity.StockId = stock.StockId;
            entity.MaterialId = stock.MaterialId;
            entity.StockAmount = totalCount;

            if (addStockAmount>0)
            {
                entity.LastInTime = DateTime.FromFileTimeUtc(+3); 
            }
            else
            {
                entity.LastOutTime = DateTime.FromFileTimeUtc(+3); 
            }
            
        
            _serviceManager.StockService.UpdateOneStock(id,entity,true);
            
        }
        
      
    }



