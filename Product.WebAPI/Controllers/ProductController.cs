using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Interfaces;
using Product.Repositories;


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
    public IActionResult GetProducts()
    {
        var products = _serviceManager.ProductService.GetProduct(false);//service
        return Ok(products);
    }
   
    [HttpGet("{id:int}")]
    public IActionResult GetProductById(int id)
    {
       
      var products = _serviceManager.ProductService.GetProductById(id,false);
        if (products is null)
        {
            return NotFound();
        }
        
        return Ok(products);
    }
    
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product.Entity.Product product)
    {
       /*  if (product is null)
        {
            return BadRequest("Invalid product data.");
        }

        var addedObj = _context.Products.Add(product); //--
        var expiryTime = DateTimeOffset.Now.AddMinutes(2);
        _cacheService.SetData<Product.Entity.Product>($"Product {product.ProductId}",addedObj.Entity,expiryTime);
        return Ok(addedObj.Entity);*/
        
       
        try
        {
            if (product is null)
            {
                return BadRequest();
            }
            _serviceManager.ProductService.CreateProduct(product);
            return Ok(product);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    [HttpPut ("{id:int}")]
    public IActionResult UpdateProductById(int id, Product.Entity.Product product)
    {
        try
        {
            if (product is null)
            {
                return BadRequest();
            }
            _serviceManager.ProductService.UpdateProductById(id,product,true);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    [HttpDelete ("{id:int}")]
    public IActionResult DeleteProductById(int id)
    {
        try
        {
            _serviceManager.ProductService.DeleteProductById(id,false);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        /* var exist = _serviceManager.ProductService.GetProductById(id, false);
            //_context.Products.FirstOrDefault(x => x.ProductId.Equals(id));
        if (exist!= null)
        {
            _serviceManager.ProductService.DeleteOneProduct(id,true);
            _cacheService.RemoveData($"Product{id}");
            return NoContent();
        }

        return NotFound();*/
    }
}