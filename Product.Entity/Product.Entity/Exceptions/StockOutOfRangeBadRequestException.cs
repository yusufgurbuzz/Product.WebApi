namespace Product.Entity.Exceptions;

public class StockOutOfRangeBadRequestException : BadRequestException
{
    public StockOutOfRangeBadRequestException() : base("Maximum stock should be less than 1000 and greater than one(1).")
    {
    }
}