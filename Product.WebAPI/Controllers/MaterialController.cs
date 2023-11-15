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
    public IActionResult GetAllMaterials()
    {
        var material = _serviceManager.MaterialService.GetAllMaterial(false);
        return Ok(material);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetOneMaterial(int id)
    {
        var material = _serviceManager.MaterialService.GetMaterialById(id,false);
        if (material is null)
        {
            return NotFound();
        }
        
        return Ok(material);
    }
    
    [HttpPost]
    public IActionResult CreateOneMaterial(Material material)
    {
        try
        {
            if (material is null)
            {
                return BadRequest();
            }
            _serviceManager.MaterialService.CreateOneMaterial(material);
            return StatusCode(201,material);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    [HttpPut ("{id:int}")]
    public IActionResult UpdateOneMaterial(int id, Material material)
    {
        try
        {
            if (material is null)
            {
                return BadRequest();
            }
            _serviceManager.MaterialService.UpdateOneMaterial(id,material,true);
            return NoContent();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    [HttpDelete ("{id:int}")]
    public IActionResult DeleteOneMaterial(int id)
    {
        try
        {
            _serviceManager.MaterialService.DeleteOneMaterial(id,false);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}