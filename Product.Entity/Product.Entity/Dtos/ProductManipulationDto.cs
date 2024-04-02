using System.ComponentModel.DataAnnotations;

namespace Product.Entity;

public abstract record ProductManipulationDto
{
    [Required(ErrorMessage = "Name is a Required Field")]
    [MinLength(2)]
    [MaxLength(50)]
    public string ProductName { get; init; }
    
    [Required(ErrorMessage = "Stock is a Required Field")]
    [Range(0,10000)]
    public int ProductStock { get; init; }
}