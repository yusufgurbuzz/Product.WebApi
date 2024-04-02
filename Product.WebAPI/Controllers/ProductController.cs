using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Entity;
using Product.Entity.LogModel;
using Product.Interfaces;
using ProductWebApi.ActionFilters;

namespace ProductWebApi.Controllers;


[Route("api/Products"), ApiController]
[ServiceFilter(typeof(LogFilterAttribute))]
public class ProductController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public ProductController(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    public async Task<ActionResult> GetProducts()
    {
        var product =  await _serviceManager.ProductService.GetProduct(false);
        return Ok(product);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var products = await _serviceManager.ProductService.GetProductById(id, false);
        return Ok(products);
    }
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost("createProduct")]
    public async Task<ActionResult> CreateProduct([FromBody] ProductInsertionDto product)
    {
        await _serviceManager.ProductService.CreateProduct(product);
        return StatusCode(201,product);
    }
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProductById(int id, UpdateProductDto product)
    {
        await _serviceManager.ProductService.UpdateProductById(id, product, true);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProductById(int id)
    {
        await _serviceManager.ProductService.DeleteProductById(id, false);
        return NoContent();
    }
}