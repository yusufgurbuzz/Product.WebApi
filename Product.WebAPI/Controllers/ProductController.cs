using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Entity;
using Product.Entity.LogModel;
using Product.Entity.RequestFeatures;
using Product.Interfaces;
using ProductWebApi.ActionFilters;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

    [HttpGet("AllProduct")]
    public async Task<ActionResult> GetProducts([FromQuery]ProductParameters productParameters)
    {
        var pagedResult =  await _serviceManager.ProductService.GetProduct(productParameters,false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.Item1);
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