using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Interfaces;
using Product.Repositories;


namespace ProductWebApi.Controllers;

[Route("api/Products"), ApiController]
public class ProductController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _context;
    
    public ProductController(IServiceManager serviceManager, ICacheService cacheService)
    {
        _serviceManager = serviceManager;
        _cacheService = cacheService;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var cacheData = _cacheService.GetData<IEnumerable<Product.Entity.Product>>("Products");
        if (cacheData !=null && cacheData.Count()>0)
        {
            return Ok(cacheData);
        }

        cacheData = _serviceManager.ProductService.GetAllProduct(false);
        var expiryTime = DateTimeOffset.Now.AddMinutes(2);
        _cacheService.SetData<IEnumerable<Product.Entity.Product>>("Products",cacheData,expiryTime);
        return Ok(cacheData);
        
        /*
        var products = _serviceManager.ProductService.GetAllProduct(false);//service
        return Ok(products);*/
    }
   
    [HttpGet("{id:int}")]
    public IActionResult GetOneProduct(int id)
    {
        var cacheData = _cacheService.GetDataById<Product.Entity.Product>(id);
        if ( cacheData is not null)
        {
            return Ok(cacheData);
        }
        cacheData = _serviceManager.ProductService.GetProductById(id,false);
        var expiryTime = DateTimeOffset.Now.AddMinutes(2);
        _cacheService.SetData<Product.Entity.Product>("Products",cacheData,expiryTime);
        return Ok(cacheData);
        
        
       /* var products = _serviceManager.ProductService.GetProductById(id,false);
        if (products is null)
        {
            return NotFound();
        }
        
        return Ok(products);*/
    }
    
    [HttpPost]
    public IActionResult CreateOneProduct([FromBody] Product.Entity.Product product)
    {
        if (product is null)
        {
            return BadRequest("Invalid product data.");
        }

        var addedObj = _context.Products.Add(product); //--
        var expiryTime = DateTimeOffset.Now.AddMinutes(2);
        _cacheService.SetData<Product.Entity.Product>($"Product {product.ProductId}",addedObj.Entity,expiryTime);
        return Ok(addedObj.Entity);
        
        /*
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
        }*/
        
        
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
        var exist = _serviceManager.ProductService.GetProductById(id, false);
            //_context.Products.FirstOrDefault(x => x.ProductId.Equals(id));
        if (exist!= null)
        {
            _serviceManager.ProductService.DeleteOneProduct(id,true);
            _cacheService.RemoveData($"Product{id}");
            return NoContent();
        }

        return NotFound();
        
        /*
        try
        {
            _serviceManager.ProductService.DeleteOneProduct(id,false);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }*/
    }
}