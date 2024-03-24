namespace Product.Entity.Exceptions;

public sealed class ProductNotFoundException : NotFoundException // kalıtıma kapattık
{
    public ProductNotFoundException(int id) : base($"The book with id : {id} could not found")
    {
    }
}