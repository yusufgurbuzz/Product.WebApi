using Microsoft.AspNetCore.Mvc;
using Product.Entity;
using Product.Interfaces;

namespace ProductWebApi.Controllers;

[Route("api/Materials"), ApiController]
public class MaterialController : Controller
{
    private readonly IServiceManager _serviceManager;
    
    public MaterialController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetMaterials()
    {
        var material = _serviceManager.MaterialService.GetMaterial(false);
        return Ok(material);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetMaterialById(int id)
    {
        var material = _serviceManager.MaterialService.GetMaterialById(id,false);
        if (material is null)
        {
            return NotFound();
        }
        
        return Ok(material);
    }
    
    [HttpPost]
    public IActionResult CreateMaterial(Material material)
    {
        try
        {
            if (material is null)
            {
                return BadRequest();
            }
            _serviceManager.MaterialService.CreateMaterial(material);
            return StatusCode(201,material);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    [HttpPut ("{id:int}")]
    public IActionResult UpdateMaterialById(int id, Material material)
    {
        try
        {
            if (material is null)
            {
                return BadRequest();
            }
            _serviceManager.MaterialService.UpdateMaterialById(id,material,true);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    [HttpDelete ("{id:int}")]
    public IActionResult DeleteMaterialById(int id)
    {
        try
        {
            _serviceManager.MaterialService.DeleteMaterialById(id,false);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}