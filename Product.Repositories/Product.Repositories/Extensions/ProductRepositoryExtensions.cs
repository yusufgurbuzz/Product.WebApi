using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
namespace Product.Repositories.Extensions;

public static class ProductRepositoryExtensions
{
    public static IQueryable<Entity.Product> FilterProducts(this IQueryable<Entity.Product> products,
        uint minStock, uint maxStock) => products.Where(p =>
        (p.ProductStock >= minStock) &&
        (p.ProductStock < maxStock));

    public static IQueryable<Entity.Product> SearchProducts(this IQueryable<Entity.Product> products, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return products;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return products.Where(p => p.ProductName
                .ToLower()
                .Contains(searchTerm));
    }

    public static IQueryable<Entity.Product> SortProducts(this IQueryable<Entity.Product> products, string? orderByQueryString )
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return products.OrderBy(p => p.ProductId);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Entity.Product>(orderByQueryString);
        
        if (orderQuery is null)
            return products.OrderBy(p => p.ProductId);

        return products.OrderBy(orderQuery);
    }
}