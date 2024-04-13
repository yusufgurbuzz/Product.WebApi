using System.Dynamic;

namespace Product.Interfaces;

public interface IDataShapper<T>
{
    IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldString);
    ExpandoObject ShapeData(T entities, string fieldString);
}