namespace Product.Entity;


public record UpdateProductDto
{
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public int ProductStock { get; init; }
}