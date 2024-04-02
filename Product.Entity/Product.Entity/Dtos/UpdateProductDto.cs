using System.ComponentModel.DataAnnotations;

namespace Product.Entity;


public record UpdateProductDto : ProductManipulationDto
{
    [Required]
    public int ProductId { get; init; }
   
}