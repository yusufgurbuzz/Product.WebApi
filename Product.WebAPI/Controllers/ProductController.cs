using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Entity;
using Product.Entity.Exceptions;
using Product.Interfaces;
using Product.Repositories;


namespace ProductWebApi.Controllers;

[Route("api/Products"), ApiController]
public class ProductController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly IMapper _mapper;

    public ProductController(IServiceManager serviceManager, IMapper mapper)
    {
        _serviceManager = serviceManager;
        _mapper = mapper;
    }

    [HttpGet("getProducts")]
    public async Task<ActionResult> GetProducts()
    {
        var product =  await _serviceManager.ProductService.GetProduct(false);
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(product));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProductById(int id)
    {
        var products = _serviceManager.ProductService.GetProductById(id, false);
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


        if (product is null)
        {
            return BadRequest();
        }

        _serviceManager.ProductService.CreateProduct(product);
        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProductById(int id, UpdateProductDto product)
    {
        if (product is null)
        {
            return BadRequest();
        }

        _serviceManager.ProductService.UpdateProductById(id, product, true);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteProductById(int id)
    {
        _serviceManager.ProductService.DeleteProductById(id, false);
        return NoContent();

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