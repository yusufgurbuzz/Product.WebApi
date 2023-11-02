using Microsoft.AspNetCore.Mvc;
using Product.Interfaces;

namespace ProductWebApi.Controllers;

[Route("api/Products"), ApiController]
public class ProductController : Controller
{
    private readonly IServiceManager _serviceManager;
    
    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _serviceManager.ProductService.GetAllProduct(false);//service
        return Ok(products);
    }
   
    [HttpGet("{id:int}")]
    public IActionResult GetOneProduct(int id)
    {
        var products = _serviceManager.ProductService.GetOneProduct(id,false);
        if (products is null)
        {
            return NotFound();
        }
        
        return Ok(products);
    }
    
    [HttpPost]
    public IActionResult CreateOneProduct(Product.Entity.Product product)
    {
        try
        {
            if (product is null)
            {
                return BadRequest();
            }
            _serviceManager.ProductService.CreateOneProduct(product);
            return Ok(product);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
        
    }

    [HttpPut ("{id:int}")]
    public IActionResult UpdateOneProduct(int id, Product.Entity.Product product)
    {
        try
        {
            if (product is null)
            {
                return BadRequest();
            }
            _serviceManager.ProductService.UpdateOneProduct(id,product,true);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    [HttpDelete ("{id:int}")]
    public IActionResult DeleteOneProduct(int id)
    {
        try
        {
            _serviceManager.ProductService.DeleteOneProduct(id,false);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}