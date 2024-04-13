namespace Product.Entity.Exceptions;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message) : base(message) //base class exception oluyor.
    {
        
    }
}